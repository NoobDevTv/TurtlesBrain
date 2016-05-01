using System;
using System.IO;
using Eloquera.Client;
using System.Security;
using System.Linq;

namespace TurtlesBrain
{
    internal static class AuthManager
    {
        static DB db = new DB("server=(local);password=;options=none;");
        internal static bool Authenticate(string username, string password, out string correctUsername)
        {
            
            correctUsername = (from Account a in db
                               where a.Password == password
                               where a.Username.ToLower().Contains(username.ToLower())
                               select a).FirstOrDefault()?.Username;

            if (correctUsername != null)
                return true;

            return false;
        }

        public static void Insert(object data)
        {
            if (db.IsOpen)
                db.Store(data);
        }

        public static void Initialize(string DBname)
        {
            if (!File.Exists($"{DBname}.eq"))
                db.CreateDatabase(DBname);
            db.OpenDatabase(DBname);

        }
    }
}