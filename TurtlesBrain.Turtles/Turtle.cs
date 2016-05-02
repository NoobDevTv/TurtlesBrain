using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TurtlesBrain.Turtles
{
    public class Turtle
    {
        private static readonly object LogLockObj = new object();
        private readonly WebClient _client = new WebClient();
        private static readonly string BaseUrl = "http://localhost:4344";

        public string Label;

        public async Task Run()
        {
            Log("Connecting");
            Label = Encoding.UTF8.GetString(await Post("turtle/hello"));
            Log("Connected");

            while (true)
            {
                var cmd = await PostAsString("turtle/queryCommand");
                Log(cmd);
                await PostString("turtle/return", $"{cmd}|true|");
            }
        }

        private async Task<byte[]> Post(string to, byte[] data = null)
        {
            return await _client.UploadDataTaskAsync(new Uri($"{BaseUrl}/{to}?label={Label}"), data ?? new byte[0]);
        }

        private async Task<byte[]> PostString(string to, string str)
        {
            return await Post(to, str == null ? null : Encoding.UTF8.GetBytes(str));
        }

        private async Task<string> PostAsString(string to, string str = null)
        {
            return Encoding.UTF8.GetString(await PostString(to, str));
        }

        public Turtle(string label)
        {
            Label = label;
        }

        private void Log(string str)
        {
            lock (LogLockObj)
            {
                Console.WriteLine("{0}: {1}", Label, str);
            }
            
        }
    }
}