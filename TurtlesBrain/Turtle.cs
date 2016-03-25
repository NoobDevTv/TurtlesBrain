using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurtlesBrain
{
    public class Turtle
    {
        public string Label { get; private set; }

        private Queue<KeyValuePair<string, TurtleServer.Result>> commands = new Queue<KeyValuePair<string, TurtleServer.Result>>();

        public KeyValuePair<string, TurtleServer.Result> GetNextCommand()
        {
            return commands.Dequeue();
        }

        public bool IsNextCommand()
        {
            if (commands.Count == 0)
                return false;

            return true;
        }

        public Turtle(string label)
        {
            Label = label;
        }
        public void AddCommand(string command, TurtleServer.Result callback)
        {
                commands.Enqueue(new KeyValuePair<string, TurtleServer.Result>(command,callback));
        }

        public bool down()
        {
            return GetBool(Send("turtle.down()"));
        }

        public bool GetBool(string theString)
        {
            return bool.Parse(theString.Split('|')[2]);
        }

        public string Send(string command)
        {
            ManualResetEvent waitHandle = new ManualResetEvent(false);

            string response = null;

            AddCommand( command, (result) =>
             {
                 response = result;
                 waitHandle.Set();
             });
            waitHandle.WaitOne();

            return response;
        }

        public void QueryCommand(HttpListenerResponse response)
        {


            while(!IsNextCommand())
            {
                Thread.Sleep(25);
            }

            KeyValuePair<string, TurtleServer.Result> nextCommand = GetNextCommand();

            lock (Program.server.commandPoolOderSo)
            {
                Program.server.commandPoolOderSo.Add(Label, nextCommand);
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(nextCommand.Key);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

    }
}
