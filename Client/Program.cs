using System;
using turtleAPI;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {
            Turtle t = new Turtle("myTurtle");
            string i;
            while (true)
            {
                string command = Console.ReadLine();
                Type type = typeof(Turtle);
                var v = type.GetMethods();
                foreach (var item in v)
                {
                    if (item.Name.ToString().Contains(command))
                    {
                        Console.WriteLine(item.Invoke(t, new object[0]));
                        break;
                    }
                }
            }
        }
    }
}