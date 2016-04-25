using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using turtleAPI;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            Server.Connect("localhost", 7777, "susch","riechtstreng");
            Console.ReadKey();
            Turtle turtle = new Turtle("suschrichtstark");
            Turtle turtle2 = new Turtle("fischNamenssuschi");
            Console.WriteLine(turtle.forward());
            //SkyWriter writer = new SkyWriter(
            //    new string[] { "turtleName1", "turtleName2", "turtleName3", "turtleName4", "turtleName5", }, 
            //    "text to write","turtle","b");
            Console.ReadKey();
        }
    
    }
}