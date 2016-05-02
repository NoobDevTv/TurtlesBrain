using System;

namespace TurtlesBrain
{
    public class Turtle : Computer
    {
        public string args;

        public Client Client { get; internal set; }

        public Turtle(string label) : base(label)
        {
        }
    }
}
