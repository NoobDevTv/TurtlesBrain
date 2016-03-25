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

namespace httpServer
{
    class TurtleServer
    {
        private WebSocketServer webServer = new WebSocketServer("ws://0.0.0.0:34197");

        private HttpListener server = new HttpListener();
        private Queue<string> commands = new Queue<string>();
        private AutoResetEvent waitForCommand = new AutoResetEvent(false);
        private Dictionary<string, Turtle> turtles = new Dictionary<string, Turtle>();
        private string lastCommand = "";
        private string createUniqueName()
        {
            string label = Guid.NewGuid().ToString().Substring(0, 8);
            while (turtles.ContainsKey(label))
                label = Guid.NewGuid().ToString().Substring(0, 8);
            return label;
        }

        private void processMessage(string message)
        {
            Turtle turtle;
            string label = message.Split('|')[0];
            string command = message.Split('|')[1];
            turtles.TryGetValue(label, out turtle);
            if (turtle != null)
                turtle.AddCommand(command);
        }
        private Turtle hello(string label, HttpListenerResponse response)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                label = createUniqueName();
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
        private void command(HttpListenerResponse response)
        {
            if (commands.Count == 0)
                waitForCommand.WaitOne();
            Console.WriteLine("Send Command");
            string command;
            lock (commands)
            {
                command = commands.Dequeue();
            }
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
        private void user(HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Properties.Resources.user);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
        public void AddCommand(string label, string command)
        {
            Turtle turtle;
            if (turtles.TryGetValue(label, out turtle))
                turtle.AddCommand(command);
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
                    if (localPath.StartsWith("/turtle/return"))
                        using (System.IO.Stream body = request.InputStream) // here we have data
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                            {
                                string s = reader.ReadToEnd();
                                if (GotResult != null)
                                    GotResult.Invoke(s);
                                foreach (var result in s.Split('|'))
                                {
                                    if (result != "nil")
                                        Console.Write(result + " ");
                                }
                                Console.WriteLine();
                            }
                        }
                    var label = request.QueryString["label"];
                    Turtle turtle;
                    if (localPath == "/turtle/hello")
                    {
                        turtle = hello(label, response);
                    }
                    else if (turtles.TryGetValue(label, out turtle))
                    {

                        if (localPath == "/turtle/command")
                        {
                            if (request.QueryString.AllKeys.Contains("value"))
                            {
                                turtle.AddCommand(request.QueryString["value"]);
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
                    user(response);
                }

                response.Close();
            }
            catch (Exception ex)
            {

            }

        }
        public void Start()
        {

            server.Prefixes.Add("http://+:4344/user/");
            server.Prefixes.Add("http://+:4344/turtle/");
            server.Prefixes.Add("http://+:4344/turtle/return/");

            server.Start();
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
                socket.OnMessage = message => processMessage(message);
                GotResult += (result) => socket.Send("result|" + result);
            });

            server.BeginGetContext(EndGetContext, null);

        }

        public delegate void Result(string result);
        public event Result GotResult;
    }
}
