using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using turtleAPI;

namespace Client
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Turtle t = new Turtle("rave1000_1");
            string i;
            Console.WriteLine(t.down(out i));
            Console.WriteLine(i);
            Console.WriteLine(t.up(out i));
            Console.WriteLine(i);
            Console.WriteLine(t.turnLeft(out i));
            Console.WriteLine(i);
            Console.WriteLine(t.turnRight(out i));
            Console.WriteLine(i);

            Console.ReadKey();
        }
    }
}
