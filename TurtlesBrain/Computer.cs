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


        private string _currentCommand;
        private string _currentResult;

        private readonly AutoResetEvent _cmdSetter = new AutoResetEvent(true);
        private readonly AutoResetEvent _cmdGetter = new AutoResetEvent(false);
        private readonly AutoResetEvent _resultGetter = new AutoResetEvent(false);

        public Computer(string label)
        {
            Label = label;
        }

        public string Execute(string command)
        {
            Program.Debug($"{Label}: Execute -> {command}");
            _cmdSetter.WaitOne();
            _currentCommand = command;
            _cmdGetter.Set();
            _resultGetter.WaitOne();
            _cmdSetter.Set();
            return _currentResult;
        }

        public string Send(string command)
        {
            return Execute(command);
        }

        public void SetResult(string result)
        {
            _currentResult = result;
            _resultGetter.Set();
        }

        public string WaitForCommand()
        {
            _cmdGetter.WaitOne();
            return _currentCommand;
        }
    }
}