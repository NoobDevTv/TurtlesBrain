using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            
            SkyWriter writer = new SkyWriter(
                new string[] { "susch1", "susch2", "susch3", "susch4", "susch5", }, 
                "text to write");
            Console.ReadKey();
        }
    
    }
}