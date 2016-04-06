using System.IO;
using System.Linq;
using System.Net;
using System.Text;
namespace turtleAPI
{
    public class Computer
    {
        public string Label { get; private set; }
        public string[] args;

        static WebRequest req;

        public Computer(string label)
        {
            Label = label;
        }

        internal string Send(string command)
        {
            req = WebRequest.Create("http://suschpc.noip.me:4344/api/command/?label=" + Label);
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

        internal void Args()
        {
            args = GetArray(getArgs(), 0);
        }

        internal string getArgs()
        {
            req = WebRequest.Create("http://suschpc.noip.me:4344/api/args/?label=" + Label);
            req.Method = "POST";
            string postData = "";
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


        internal bool GetBool(string theString)
        {
            return bool.Parse(theString.Split('|')[1]);
        }

        internal byte GetByte(string theString)
        {
            return byte.Parse(theString.Split('|')[1]);
        }

        internal int GetInt(string theString)
        {
            return int.Parse(theString.Split('|')[1]);
        }

        internal string GetReason(string theString)
        {
            return theString.Split('|')[2];
        }

        internal string[] GetArray(string theString, int skipAmount)
        {
            return theString.Split('|').Skip(skipAmount).ToArray();
        }
    }
}