using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public List<Turtle> AllTurtles => turtles.Values.ToList();
        public Turtle this[string label]
        {
            get
            {
                Console.WriteLine(label);
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
        }
        
        protected override void Dispatch(ITurtleApiMessage msg)
        {
            
            if (msg.GetType() == typeof(TurtleMessage))
            {
                
                var tm = (TurtleMessage)msg;
                Console.WriteLine($"TurtleMessage: {tm.Label}");
                if (!turtles.ContainsKey(tm.Label))
                    turtles[tm.Label] = new Turtle(tm.Label, this);
            }
            else if (msg.GetType() == typeof(Response))
            {
                var rs = (Response)msg;
                Console.WriteLine($"Response: {rs.Label} -> {rs.Content}");
                turtles[rs.Label].OnMessage(rs.Content);
            }

        }
    }
}
