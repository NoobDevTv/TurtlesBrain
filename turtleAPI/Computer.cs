using System.IO;
using System.Linq;
using System.Net;
using System.Text;
namespace turtleAPI
{
    public class Computer
    {
        public string Label { get; private set; }

        static WebRequest req;

        public Computer(string label)
        {
            Label = label;            
        }

        public string Send(string command)
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

        public string[] GetArray(string theString)
        {
            return theString.Split('|').ToArray<string>();
        }
    }
}