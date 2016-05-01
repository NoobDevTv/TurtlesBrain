using Eloquera.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtlesBrain;

namespace DBTeast
{
    class Program
    {
        static void Main(string[] args)
        {
             DB db = new DB("server=(local);password=;options=none;");
             if (!File.Exists("ourDB.eq"))
                 db.CreateDatabase("ourDB");

             db.OpenDatabase("ourDB");

             
             db.Store(new Account("susch", "thisisapasswortfortheturtle"));
            
             db.Close();           

            Console.ReadKey();
        }
    }

}
