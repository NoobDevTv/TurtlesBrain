using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace httpServer
{
    class Turtle
    {
        private Queue<string> commands = new Queue<string>();
        private AutoResetEvent waitForCommand = new AutoResetEvent(false);
        public Turtle(string label)
        {
            Label = label;
        }
        public string Label { get; private set; }
        public void AddCommand( string command)
        {
            lock (commands)
            {
                commands.Enqueue(command);

            }
            waitForCommand.Set();
        }
        public void QueryCommand(HttpListenerResponse response)
        {
            if (commands.Count == 0)
                waitForCommand.WaitOne();
            string command;
            lock (commands)
            {
                command = commands.Dequeue();
            }
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
    }
}
