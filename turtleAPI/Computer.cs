using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace turtleAPI
{
    public class Computer
    {
        public string Label { get; private set; }

        static WebRequest req;
        public Computer(string label)
        {
            Label = label;
            req = WebRequest.Create("http://suschpc.noip.me:4344/api/command/?label=" + label);
        }

        public string Send(string command)
        {
            req.Method = "POST";
            string postData = command;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            req.ContentLength = byteArray.Length;
            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = req.GetResponse();
            dataStream = response.GetResponseStream();
            dataStream.Close();
            response.Close();
            return response.Headers.Get("erfolg");
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