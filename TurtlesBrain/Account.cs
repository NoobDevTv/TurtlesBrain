using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace TurtlesBrain
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Account(string username, string pw)
        {
            Username = username;
            Password = pw;
        }
    }
}
