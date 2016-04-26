using System;
using System.Net.Sockets;
using TurtlesBrain.Shared;

namespace TurtlesBrain
{
    public class Client : TurtleApiConnection
    {
        private ClientInfo info;
        private NetworkStream stream;

        public string Username => info.Name;

        public Client(ClientInfo info, NetworkStream stream) : base(stream)
        {
            this.info = info;
            this.stream = stream;
        }

        protected override void Dispatch(ITurtleApiMessage msg)
        {
            Console.WriteLine(msg);
            var cm = msg as ClientMessage;
            if (cm != null)
            {
                ClientServer.Execute(cm.Label, cm.Command);
            }
        }
    }
}