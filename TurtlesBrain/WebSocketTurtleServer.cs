using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtlesBrain
{
    public static class WebSocketTurtleServer
    {
        private static IWebSocketConnection socket;
        private static WebSocketServer webServer;
        private static WebSocketHttpRequest req;

        public static void Start(int port)
        {
            webServer = new WebSocketServer($"ws://0.0.0.0:{port}");
            req = new WebSocketHttpRequest();

            webServer.Start(internalSocket =>
            {
                socket = internalSocket;
                internalSocket.OnOpen = () => {
                    UpdateList();
                    Program.Info("Socket opened");
                };
                internalSocket.OnClose = () => Program.Info("Socket closed");
                internalSocket.OnMessage = ProcessMessage;
                TurtleServer.GotResult += (label, result) => internalSocket.Send("result|" + label + "|" + result);
            });
        }

        private static IEnumerable<string> LabelList => new[] { "list" }.Concat(ClientServer.Turtles.Select(t => t.Label));
        public static void UpdateList() => socket.Send(string.Join("|", LabelList));

        private static void ProcessMessage(string message)
        {
            Turtle turtle;
            string label = message.Split('|')[0];
            string command = message.Split('|')[1];
            if(ClientServer.TryGetTurtle(label, out turtle))
                turtle.Execute(command);
        }
    }
}
