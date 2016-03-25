using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using System.IO;
using System.Threading;

namespace httpServer
{
    class Program
    {

        static void Main(string[] args)
        {//suschpc.noip.me
            TurtleServer server = new TurtleServer();

            server.Start();
            Console.WriteLine("Success");
            
            while(true)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (command == "exit")
                    break;

                //server.AddCommand(command);
            }
        }
    }
}
