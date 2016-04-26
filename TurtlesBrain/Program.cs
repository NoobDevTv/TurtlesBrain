using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using TurtlesBrain.Shared;

namespace TurtlesBrain
{
    public class Program
    {
        public static TurtleServer turtleserver;
        public static WebSocketTurtleServer webserver;


        static void Main(string[] args)
        {
            MessageConverter.Initialize();
            Thread TurtleServerThread = new Thread(o => { turtleserver = new TurtleServer(); });
            TurtleServerThread.Name = "ServerThread";
            TurtleServerThread.Start();

            ClientServer.Start(7777);


            Console.WriteLine("Success");
            Thread WebsocketTurtleThread = new Thread(o => { webserver = new WebSocketTurtleServer(); });
            WebsocketTurtleThread.Name = "WebsocketThread";
            WebsocketTurtleThread.Start();
            //while (true)
            //{
            //    Console.Write(">");
            //    string command = Console.ReadLine();
            //    if (command == "exit")
            //        break;

            //}

            Console.ReadLine();
        }

    }
}