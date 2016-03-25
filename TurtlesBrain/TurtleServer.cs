using Fleck;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace TurtlesBrain
{
    public class TurtleServer
    {
        private WebSocketServer webServer = new WebSocketServer("ws://0.0.0.0:34197");
        private HttpListener server = new HttpListener();
        public Dictionary<string, KeyValuePair<string, Result>> commandPoolOderSo = new Dictionary<string, KeyValuePair<string, Result>>();
        private Dictionary<string, Turtle> turtles = new Dictionary<string, Turtle>();

        // Dictionary mit turtle lable (key) und callback (aktueller commmand), wenn leer dann, kommt neues von queue. Queue hält Callbacks und Command
        // Bei response schaut server in Dictionary was aktueller Callback ist, entfernt aus Dictionary und lädt nach. Server hat Pool mit aktuellen
        //command 
        public void Start()
        {
            server.Prefixes.Add("http://+:4344/user/");
            server.Prefixes.Add("http://+:4344/turtle/");

            server.Start();
            WebServerStart();
            server.BeginGetContext(EndGetContext, null);
        }

        private Turtle Handshake(string label, HttpListenerResponse response)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                label = CreateUniqueName();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("os.setComputerLabel(" + label + ")");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(":)");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            var turtle = new Turtle(label);
            turtles.Add(label, turtle);
            return turtle;
        }

        private string CreateUniqueName()
        {

            string label = Guid.NewGuid().ToString().Substring(0, 8);
            while (turtles.ContainsKey(label))
                label = Guid.NewGuid().ToString().Substring(0, 8);
            return label;
        }

        #region Weboberfläche
        private void User(HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Properties.Resources.user);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
        private void WebServerStart()
        {
            webServer.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    string turtleList = "list";
                    foreach (var item in turtles)
                    {
                        turtleList += "|" + item.Value.Label;
                    }
                    socket.Send(turtleList);
                    Console.WriteLine("Socket opened");
                };
                socket.OnClose = () => Console.WriteLine("Socket closed");
                socket.OnMessage = message => ProcessMessage(message);
                GotResult += (result) => socket.Send("result|" + result);
            });
        }

        private void ProcessMessage(string message)
        {

            Turtle turtle;
            string label = message.Split('|')[0];
            string command = message.Split('|')[1];
            turtles.TryGetValue(label, out turtle);
            if (turtle != null)
                turtle.AddCommand(command, (result) => { });
        }
        #endregion

        //public void QueryCommand(Turtle turtle, HttpListenerResponse response)
        //{


        //    if (turtle.IsNextCommand()) {
        //        response.Abort();
        //        return;
        //    }

        //    KeyValuePair<string, Result> nextCommand = turtle.GetNextCommand();

        //    commandPoolOderSo.Add(turtle.Label, nextCommand);

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(nextCommand.Key);
        //    response.ContentLength64 = buffer.Length;
        //    response.OutputStream.Write(buffer, 0, buffer.Length);
        //}

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
                    var label = request.QueryString["label"];

                    if (localPath.StartsWith("/turtle/return"))
                    {
                        using (System.IO.Stream body = request.InputStream)
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                            {
                                string s = reader.ReadToEnd();
                                if (GotResult != null)
                                    GotResult.Invoke(s);

                                if (commandPoolOderSo.ContainsKey(label))
                                {
                                    commandPoolOderSo.First(c => c.Key == label).Value.Value(s);
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
                                turtle.AddCommand(request.QueryString["value"], (result) => { });
                            }
                        }
                        else if (localPath == "/turtle/queryCommand")
                        {
                            turtle.QueryCommand(response);
                        }
                        else if (localPath == "/turtle/delete")
                        {
                            turtles.Remove(label);
                        }

                    }
                }
                else if (localPath == "/user")
                {
                    User(response);
                }

                response.Close();
            }
            catch (Exception ex)
            {

            }

        }

        public delegate void Result(string result);
        public event Result GotResult;
    }
}
