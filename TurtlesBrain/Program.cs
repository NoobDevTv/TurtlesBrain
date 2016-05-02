using System;
using System.Configuration;
using System.Net;
using System.Threading;
using TurtlesBrain.Shared;

namespace TurtlesBrain
{
    public class Program
    {

        private static readonly object ConsoleLock = new object();

        static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.MaxServicePoints = 1000;

            ThreadPool.SetMinThreads(10, 10);

            MessageConverter.Initialize();

            int i;
            int.TryParse(ConfigurationManager.AppSettings.Get("TurtleServerPort"), out i);
            TurtleServer.Start(i);
            int.TryParse(ConfigurationManager.AppSettings.Get("ClientServerPort"), out i);
            ClientServer.Start(i);
            int.TryParse(ConfigurationManager.AppSettings.Get("WebSocketTurtleServerPort"), out i);
            WebSocketTurtleServer.Start(i);
            AuthManager.Initialize("ourDB");

            Info("Success");

            Console.ReadLine();
        }

        public static void Debug(string msg)
        {
            //Log("DEBUG", msg);
        }

        public static void Warn(string msg)
        {
            Log("WARNING", msg);
        }

        public static void Info(string msg)
        {
            Log("INFO", msg);
        }

        public static void Error(string msg)
        {
            Log("ERROR", msg);
        }

        public static void Log(string level, string msg)
        {
            // TODO: Use log4net or something :P
            lock (ConsoleLock)
                Console.WriteLine($"{level}: {msg}");
        }

        public static void Verbose(string s)
        {
            Log("VERBOSE",s);
        }
    }
}