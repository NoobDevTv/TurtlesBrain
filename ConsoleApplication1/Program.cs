using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtleAPI;
using static System.Configuration.ConfigurationManager;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            var server = Server.Connect(AppSettings.Get("ServerIp"), Convert.ToInt32(AppSettings.Get("ServerPort")),
                AppSettings.Get("Username"), AppSettings.Get("Password"));


            Console.ReadKey();
            Turtle tu = server["susch1"];
            Console.ReadKey();
            tu.turnLeft();
            Console.WriteLine("asd");
            Console.ReadKey();
        }
    }
}
