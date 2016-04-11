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
        private Queue<KeyValuePair<string, TurtleServer.Result>> commands;
        public string args;

        public Turtle(string label) : base(label)
        {
            commands = new Queue<KeyValuePair<string, TurtleServer.Result>>();
        }
    }
}
