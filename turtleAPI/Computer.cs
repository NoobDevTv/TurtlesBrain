using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System;
using System.Net.Sockets;
//using RedCorona.Net;
using System.Threading;
using TurtlesBrain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace turtleAPI
{
    public class Server : TurtleApiConnection
    {
        public static Server Connect(string ip, int port, string username, string password)
        {
            MessageConverter.Initialize();

            var t = Connection.Setup( ip,  port,  username,  password);
            t.Wait();
            return t.Result;
        }

        public Turtle this[string label]
        {
            get
            {
                return _turtles[label]; //always trust the user :<
            }
        }

        internal Server(NetworkStream stream) : base(stream)
        {
            //WriteAsync(new PongMessage());
        }

        //Directory<Type, Action<ITurtleApiMessage>> actions 
        private Dictionary<string, Turtle> _turtles = new Dictionary<string, Turtle>();

        protected override void Dispatch(ITurtleApiMessage msg)
        {
            Console.WriteLine(msg);
            if(msg.GetType() == typeof(TurtleMessage))
            {
                var tm = (TurtleMessage)msg;
                _turtles[tm.Label] = new Turtle(tm.Label, this);
            }
            else if(msg.GetType() == typeof(Response))
            {
                var rs = (Response)msg;
                _turtles[rs.Label].OnMessage(rs.Content);
            }

        }
    }

    public class Computer 
    {
        internal Computer(string label, Server server)
        {
            Label = label;
            _server = server;
        }
        

        public string Label { get; private set; }
        //public string[] args { get { return GetArray(getArgs(), 0); } set { args = value; } }
        
        //static Socket socket = Sockets.CreateTCPSocket("suschpc.noip.me", 7777);
        //static ClientInfo client = new ClientInfo(socket, null, null, ClientDirection.Both, false, EncryptionType.ServerRSAClientKey);
        AutoResetEvent wait = new AutoResetEvent(false);
        string returnString = "";
        private Server _server;

        //public Computer(string label)
        //{
        //    Label = label;
        //    //client.MessageType = MessageType.CodeAndLength;
        //    //client.OnReadMessage += Client_OnReadMessage;
        //    //client.OnReady += Client_OnReady;
        //    //client.BeginReceive();
            
        //}

        //private void Client_OnReady(ClientInfo ci)
        //{
        //    Console.WriteLine("Client is now ready");
        //}

        public void OnMessage(string content)
        {
            returnString = content;
            wait.Set();
        }


        public string Send(string command)
        {
            _server.WriteAsync(new ClientMessage { Label = Label, Command = command }).Wait();
            wait.WaitOne();            
            return returnString;
        }

        //internal string getArgs()
        //{

        //    byte[] temp = Encoding.UTF8.GetBytes(Label);
        //    client.SendMessage(2, temp);
        //    wait.WaitOne();
        //    return returnString;
        //}

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