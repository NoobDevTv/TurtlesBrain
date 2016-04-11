using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            SkyWriter writer = new SkyWriter(
                new string[] { "turtleName1", "turtleName2", "turtleName3", "turtleName4", "turtleName5", }, 
                "text to write");
            Console.ReadKey();
        }
    }
}