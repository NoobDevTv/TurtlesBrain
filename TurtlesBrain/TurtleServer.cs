using Fleck;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace TurtlesBrain
{
    public class TurtleServer
    {
        private HttpListener server;
        public Dictionary<string, KeyValuePair<string, Result>> commandPoolOderSo;
        public Dictionary<string, Turtle> turtles;

        public TurtleServer()
        {
            server = new HttpListener();
            commandPoolOderSo = new Dictionary<string, KeyValuePair<string, Result>>();
            turtles = new Dictionary<string, Turtle>();


            server.Prefixes.Add("http://+:4344/user/");
            server.Prefixes.Add("http://+:4344/turtle/");
            server.Prefixes.Add("http://+:4344/api/");
            server.Start();

            server.BeginGetContext(EndGetContext, null);
        }

        private Turtle Handshake(string label, HttpListenerResponse response)
        {
            if (turtles.ContainsKey(label))
                label = CreateUniqueName();

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(label);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);

            var turtle = new Turtle(label);
            turtles.Add(label, turtle);

            Program.webserver.UpdateList();
            return turtle;
        }

        private string CreateUniqueName()
        {

            string label = Guid.NewGuid().ToString().Substring(0, 8);
            while (turtles.ContainsKey(label))
                label = Guid.NewGuid().ToString().Substring(0, 8);
            return label;
        }

        private void User(HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Properties.Resources.user);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void EndGetContext(IAsyncResult res)
        {
            server.BeginGetContext(EndGetContext, null);
            try
            {
                var context = server.EndGetContext(res);
                var request = context.Request;
                var response = context.Response;
                string localPath = request.Url.LocalPath.TrimEnd('/');
                if (localPath.StartsWith("/turtle/") && request.QueryString.AllKeys.Contains("label"))
                {
                    Console.Write(">");
                    var label = request.QueryString["label"];

                    if (localPath.StartsWith("/turtle/return"))
                    {
                        using (System.IO.Stream body = request.InputStream)
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                            {
                                string s = reader.ReadToEnd();
                                if (GotResult != null)
                                    GotResult.Invoke(label, s);

                                if (commandPoolOderSo.ContainsKey(label))
                                {
                                    commandPoolOderSo.First(c => c.Key == label).Value.Value(label, s);
                                    commandPoolOderSo.Remove(label);
                                }

                                foreach (var result in s.Split('|'))
                                {
                                    if (result != "nil")
                                        Console.Write(result + " ");
                                }
                                Console.WriteLine();
                            }
                        }
                    }

                    Turtle turtle;
                    if (localPath == "/turtle/hello")
                    {
                        turtle = Handshake(label, response);
                    }
                    else if (turtles.TryGetValue(label, out turtle))
                    {
                        if (turtle == null)
                        {
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("register");
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        else if (localPath == "/turtle/command")
                        {
                            if (request.QueryString.AllKeys.Contains("value"))
                            {
                                turtle.AddCommand(request.QueryString["value"], (test, result) => { });
                            }
                        }
                        else if (localPath == "/turtle/queryCommand")
                        {
                            turtle.QueryCommand(response);
                        }
                        else if (localPath == "/turtle/disconnect")
                        {
                            turtles.Remove(label);
                            Program.webserver.UpdateList();
                        }

                    }
                }
                else if (localPath == "/user")
                {
                    User(response);
                }
                else if (localPath.StartsWith("/api/") && request.QueryString.AllKeys.Contains("label"))
                {
                    Turtle turtle;
                    var label = request.QueryString["label"];
                    if (turtles.TryGetValue(label, out turtle))
                    {
                        if (localPath == "/api/command" && request.QueryString.AllKeys.Contains("label"))
                        {
                            using (System.IO.Stream body = request.InputStream)
                            {
                                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                                {
                                    string s = reader.ReadToEnd();
                                    response.AddHeader("erfolg", turtle.Send(s));
                                    
                                }
                            }
                        }
                    }
                }

                response.Close();
            }
            catch (Exception ex)
            {

            }

        }

        public delegate void Result(string label, string result);
        public event Result GotResult;
    }
}
