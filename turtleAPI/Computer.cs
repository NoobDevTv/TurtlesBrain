using System.Linq;
using System.Threading;
using TurtlesBrain.Shared;

namespace turtleAPI
{
    /// <summary>
    /// represents a Computer in the Minecraft world.
    /// The Computer is the main block of ComputerCraft
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// The Name of your Computer
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// represents a Computer in the Minecraft world.
        /// The Computer is the main block of ComputerCraft.
        /// This initialize the connection to your computer
        /// </summary>
        /// <param name="label">the label of the Computer</param>
        /// <param name="server">the API Server which holds the connection to the Minecraftserver</param>
        public Computer(string label, Server server)
        {
            Label = label;
            _server = server;
        }


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

        /// <summary>
        /// Sends a Command to this Computer
        /// </summary>
        /// <param name="command">The command to you wish to send</param>
        /// <returns></returns>
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