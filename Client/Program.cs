using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            
            SkyWriter writer = new SkyWriter(
                new string[] { "jkl1", "jkl2", "jkl3", "jkl4", "jkl5", }, 
                "text to write","jkl","b");
            Console.ReadKey();
        }
    
    }
}