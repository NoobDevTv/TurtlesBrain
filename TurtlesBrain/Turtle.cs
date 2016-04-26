using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
