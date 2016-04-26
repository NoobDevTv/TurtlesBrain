using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace TurtlesBrain
{
    public class Computer
    {
        public string Label { get; private set; }

        private ConcurrentQueue<KeyValuePair<string, TurtleServer.Result>> commands;

        private HttpListenerResponse currentResponse;
        private readonly ManualResetEventSlim foo;
        public ConcurrentQueue<KeyValuePair<string, TurtleServer.Result>> ActiveCommands;

        public Computer(string label)
        {
            Label = label;

            commands = new ConcurrentQueue<KeyValuePair<string, TurtleServer.Result>>();
            foo = new ManualResetEventSlim(false);
            ActiveCommands = new ConcurrentQueue<KeyValuePair<string, TurtleServer.Result>>();
        }

        public void AddCommand(string command, TurtleServer.Result callback)
        {
            commands.Enqueue(new KeyValuePair<string, TurtleServer.Result>(command, callback));
            foo.Set();

            if (commands.Count != 1)

                Console.WriteLine($"{Label} NOT 1 COUNT QUEU BLA ASDJKALSJDKLASJDKLAJSKLD {commands.Count}");
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

        public string Send(string command, int timeout)
        {
            ManualResetEvent waitHandle = new ManualResetEvent(false);

            string response = null;

            AddCommand(command, (label, result) =>
            {
                response = result;
                waitHandle.Set();
            });
            waitHandle.WaitOne(timeout);
            return response;
        }

        public void QueryCommand(HttpListenerResponse response)
        {
            foo.Wait();

            KeyValuePair<string, TurtleServer.Result> nextCommand;
            if (!commands.TryDequeue(out nextCommand))
                throw new InvalidOperationException("");
            ActiveCommands.Enqueue(nextCommand);
            if (commands.IsEmpty)
                foo.Reset();

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(nextCommand.Key);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

    }
}