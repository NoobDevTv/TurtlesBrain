using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TurtlesBrain.Properties;

namespace TurtlesBrain
{
    public static class TurtleServer
    {
        public delegate void Result(string label, string result);

        public delegate void TurtleRequestHandler(string label, HttpListenerRequest request, HttpListenerResponse response);

        private static readonly HttpListener Server = new HttpListener();

        private static readonly Dictionary<string, TurtleRequestHandler> TurtleRequestHandlers = new Dictionary<string, TurtleRequestHandler> {
            {"turtle/hello", HelloHandler},
            {"turtle/return", ReturnHandler},
            {"turtle/command", CommandHandler},
            {"turtle/queryCommand", QueryCommandHandler},
            {"turtle/disconnect", DisconnectHandler}
        };

        public static void Start(int port)
        {
            Server.Prefixes.Add($"http://+:{port}/user/");
            Server.Prefixes.Add($"http://+:{port}/turtle/");

            Server.Start();
            Server.BeginGetContext(EndGetContext, null);
        }

        private static string CreateUniqueName() => Guid.NewGuid().ToString();

        private static void User(HttpListenerResponse response)
        {
            var buffer = Encoding.UTF8.GetBytes(Resources.user);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static string RequestBodyAsString(HttpListenerRequest request)
        {
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                return reader.ReadToEnd();
            }
        }

        private static void HelloHandler(string label, HttpListenerRequest request, HttpListenerResponse response)
        {
            Program.Debug($"HELLO: {label}");

            Turtle turtle;
            if (ClientServer.TryGetTurtle(label, out turtle))
            {
                var newName = CreateUniqueName();
                Program.Warn($"Already found {label} -> {newName}");
                label = newName;
            }
            turtle = new Turtle(label);
            ClientServer.AddTurtle(turtle);
            turtle.args = RequestBodyAsString(request);
            CloseResponse(response, label);
            Program.Debug($"CLOSED {label}");
        }

        private static void CloseResponse(HttpListenerResponse response, string content)
        {
            response.Close(Encoding.UTF8.GetBytes(content), false);
        }

        private static void CloseResponse(HttpListenerResponse response)
        {
            response.Close();
        }

        private static void ReturnHandler(string label, HttpListenerRequest request, HttpListenerResponse response)
        {
            var s = RequestBodyAsString(request);
            GotResult?.Invoke(label, s);

            Turtle commandTurtle;
            if (ClientServer.TryGetTurtle(label, out commandTurtle))
            {
                commandTurtle.SetResult(s);
                CloseResponse(response);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static void CommandHandler(string label, HttpListenerRequest request, HttpListenerResponse response)
        {
            //if (request.QueryString.AllKeys.Contains("value"))
            //{
            //    turtle.AddCommand(request.QueryString["value"], (test, result) => { });
            //}
            CloseResponse(response);
        }

        private static void QueryCommandHandler(string label, HttpListenerRequest request, HttpListenerResponse response)
        {
            Program.Debug($"QUERY: {label}");
            Turtle turtle;
            if (ClientServer.TryGetTurtle(label, out turtle))
            {
                CloseResponse(response, turtle.WaitForCommand());
            }
            else
            {
                Program.Warn($"QUERY TURTLE NOT FOUND: {label}");
                CloseResponse(response);
            }
        }

        private static void DisconnectHandler(string label, HttpListenerRequest request, HttpListenerResponse response)
        {
            ClientServer.RemoveTurtle(label);
            WebSocketTurtleServer.UpdateList();
            CloseResponse(response);
        }

        private static void EndGetContext(IAsyncResult res)
        {
            HttpListenerContext context;
            try
            {
                context = Server.EndGetContext(res);
            }
            finally
            {
                Server.BeginGetContext(EndGetContext, null);
            }

            var request = context.Request;
            var response = context.Response;

            var localPath = request.Url.LocalPath.TrimEnd('/').TrimStart('/');
            
            if (localPath.StartsWith("turtle/"))
            {
                if (!request.QueryString.AllKeys.Contains("label"))
                {
                    Program.Warn($"NO LABEL WHY: {localPath}");
                    CloseResponse(response);
                    return;
                }
                
                var label = request.QueryString["label"];
                Program.Info($"{label} - {localPath}");
                TurtleRequestHandlers[localPath](label, request, response);
            }
            else if (localPath == "user")
            {
                User(response);
            }
            else
            {
                Program.Error($"WHAT: {localPath}");
                CloseResponse(response);
            }
        }

        public static event Result GotResult;
    }
}