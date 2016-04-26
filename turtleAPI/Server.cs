using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using TurtlesBrain.Shared;

namespace turtleAPI
{
    public class Server : TurtleApiConnection
    {
        internal static Server Instance;
        private static ConcurrentDictionary<string, Turtle> turtles;

        public static Server Connect(string ip, int port, string username, string password)
        {
            MessageConverter.Initialize();

            turtles = new ConcurrentDictionary<string, Turtle>();

            var t = Connection.Setup(ip, port, username, password);
            t.Wait();
            Instance = t.Result;
            return t.Result;
        }

        public Turtle this[string label]
        {
            get
            {
                var maxTimeOut = TimeSpan.FromMinutes(2).TotalMilliseconds + 100;
                Turtle turtle;
                while (!turtles.TryGetValue(label, out turtle) && (maxTimeOut -= 100) > 0)
                {
                    Thread.Sleep(100);
                }

                return turtle;
            }
        }

        internal Server(NetworkStream stream) : base(stream)
        {
            //WriteAsync(new PongMessage());
        }

        //Directory<Type, Action<ITurtleApiMessage>> actions 
        protected override void Dispatch(ITurtleApiMessage msg)
        {
            Console.WriteLine(msg);
            if (msg.GetType() == typeof(TurtleMessage))
            {
                var tm = (TurtleMessage)msg;
                turtles[tm.Label] = new Turtle(tm.Label, this);
            }
            else if (msg.GetType() == typeof(Response))
            {
                var rs = (Response)msg;
                turtles[rs.Label].OnMessage(rs.Content);
            }

        }
    }
}
