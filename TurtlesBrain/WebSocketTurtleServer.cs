using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtlesBrain
{
    public class WebSocketTurtleServer
    {
        private IWebSocketConnection socket;
        private WebSocketServer webServer;
        private WebSocketHttpRequest req;

        public WebSocketTurtleServer()
        {
            webServer = new WebSocketServer("ws://0.0.0.0:34197");
            req = new WebSocketHttpRequest();

            webServer.Start(internalSocket =>
            {
                socket = internalSocket;
                internalSocket.OnOpen = () =>
                {
                    string turtleList = "list";
                    foreach (var item in Program.server.turtles)
                    {
                        turtleList += "|" + item.Value.Label;
                    }
                    internalSocket.Send(turtleList);
                    Console.WriteLine("Socket opened");
                };
                internalSocket.OnClose = () => Console.WriteLine("Socket closed");
                internalSocket.OnMessage = message => ProcessMessage(message);
                Program.server.GotResult += (label, result) => internalSocket.Send("result|" + label + "|" + result);
            });
        }

        public void UpdateList()
        {
            string turtleList = "list";
            foreach (var item in Program.server.turtles)
            {
                turtleList += "|" + item.Value.Label;
            }
            socket.Send(turtleList);
        }

        private void ProcessMessage(string message)
        {

            Turtle turtle;
            string label = message.Split('|')[0];
            string command = message.Split('|')[1];
            Program.server.turtles.TryGetValue(label, out turtle);
            if (turtle != null)
            {
                turtle.AddCommand(command, (test, result) => { });
            }
        }

    }
}
