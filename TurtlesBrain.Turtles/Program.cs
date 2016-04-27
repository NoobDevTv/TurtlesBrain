using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurtlesBrain.Turtles
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;
            System.Net.ServicePointManager.MaxServicePoints = 1000;

            Thread.Sleep(1000);
            foreach (var s in new [] { "jkl1","jkl2","jkl3","jkl4","jkl5", "jkl5"})
            {
                new Turtle(s).Run();
                Thread.Sleep(100);
            }
           
            Console.ReadLine();
        }
    }
}
