using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System;
using System.Net.Sockets;
using RedCorona.Net;
using System.Threading;

namespace turtleAPI
{
    public class Computer
    {
        public string Label { get; private set; }
        public string[] args { get { return GetArray(getArgs(), 0); } set { args = value; } }

        static WebRequest req;
        static Socket socket = Sockets.CreateTCPSocket("suschpc.noip.me", 7777);
        static ClientInfo client = new ClientInfo(socket,null,null,ClientDirection.Both,false,EncryptionType.ServerRSAClientKey);
        AutoResetEvent wait = new AutoResetEvent(false);
        string returnString ="";

        public Computer(string label)
        {
            Label = label;
            client.MessageType = MessageType.CodeAndLength;
            client.OnReadMessage += Client_OnReadMessage;
            client.OnReady += Client_OnReady;
            client.BeginReceive();
            
        }

        private void Client_OnReady(ClientInfo ci)
        {
            Console.WriteLine("Client is now ready");
        }

        private void Client_OnReadMessage(ClientInfo ci, uint code, byte[] bytes, int len)
        {
            returnString = Encoding.UTF8.GetString(bytes);
            wait.Set();
        }


        public string Send(string command)
        {
            byte[] temp = Encoding.UTF8.GetBytes(Label + "|" + command);
            client.SendMessage(1, temp);
            wait.WaitOne();
            return returnString;
        }

        internal string getArgs()
        {

            byte[] temp = Encoding.UTF8.GetBytes(Label);
            client.SendMessage(2, temp);
            wait.WaitOne();
            return returnString;
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

        public string[] GetArray(string theString, int skipAmount)
        {
            return theString.Split('|').Skip(skipAmount).ToArray();
        }
    }
}