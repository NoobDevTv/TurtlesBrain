using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace TurtlesBrain
{
    public class Computer
    {
        public string Label { get; private set; }

        private Queue<KeyValuePair<string, TurtleServer.Result>> commands = new Queue<KeyValuePair<string, TurtleServer.Result>>();

        public KeyValuePair<string, TurtleServer.Result> GetNextCommand()
        {
            if (commands.Count >= 2)
            {
                Console.WriteLine(commands.Count);
                return commands.Dequeue();
            }
            return commands.Dequeue();
        }

        public Computer(string label)
        {
            Label = label;
        }

        public void AddCommand(string command, TurtleServer.Result callback)
        {
            commands.Enqueue(new KeyValuePair<string, TurtleServer.Result>(command, callback));
        }

        public string Send(string command)
        {
            ManualResetEvent waitHandle = new ManualResetEvent(false);

            string response = null;

            AddCommand(command, (label, result) =>
            {
                response = result;
                waitHandle.Set();
            });
            waitHandle.WaitOne();
            return response;
        }

        public void QueryCommand(HttpListenerResponse response)
        {


            while (commands.Count == 0)
            {
                Thread.Sleep(10);
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

        public bool GetBool(string theString)
        {
            return bool.Parse(theString.Split('|')[1]);
        }

        public byte GetByte(string theString)
        {
            return byte.Parse(theString.Split('|')[1]);
        }

        public int GetInt(string theString)
        {
            return int.Parse(theString.Split('|')[1]);
        }

        public string GetReason(string theString)
        {
            return theString.Split('|')[2];
        }
    }
}