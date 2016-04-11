using System;
using System.Threading;

namespace TurtlesBrain
{
    public class Program
    {
        public static TurtleServer server;
        public static WebSocketTurtleServer webserver;

        static void Main(string[] args)
        {
            
            Thread TurtleServerThread = new Thread(o => { server = new TurtleServer(); });
            TurtleServerThread.Name = "ServerThread";
            TurtleServerThread.Start();

            Console.WriteLine("Success");
            Thread WebsocketTurtleThread = new Thread(o => { webserver = new WebSocketTurtleServer(); });
            WebsocketTurtleThread.Name = "WebsocketThread";
            WebsocketTurtleThread.Start();
            while(true)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (command == "exit")
                    break;
                
            }
        }
    }
}