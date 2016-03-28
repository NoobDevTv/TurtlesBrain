using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using System.IO;
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
            Thread WebsocketTurtleThread = new Thread(o => {webserver = new WebSocketTurtleServer(); });
            WebsocketTurtleThread.Name = "WebsocketThread";
            WebsocketTurtleThread.Start();
            while(true)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (command == "exit")
                    break;
                else if (command == "script")
                {
                    Thread thread = new Thread(sdothis);
                    thread.Name = "ScriptThread";
                    thread.Start();
                    Thread thread2 = new Thread(sdothis2);
                    thread2.Name = "ScriptThread2";
                    thread2.Start();
                    Thread thread3 = new Thread(sdothis3);
                    thread3.Name = "ScriptThread3";
                    thread3.Start();
                }

                //    server.AddCommand(command);
            }
        }

        private static void sdothis()
        {
            Script s = new Script("myTurtle");
            s.doThis();
        }
        private static void sdothis2()
        {
            Script s = new Script("tester");
            s.doThis();
        }
        private static void sdothis3()
        {
            Script s = new Script("rave1000_1");
            s.doThis();
        }
    }
}