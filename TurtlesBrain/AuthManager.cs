using System;
using System.IO;
using System.Security;
using System.Linq;
using LiteDB;
using System.Collections.Generic;

namespace TurtlesBrain
{
    internal static class AuthManager
    {
        static LiteDatabase db;
        static LiteCollection<Account> accounts;
        internal static bool Authenticate(string username, string password, out string correctUsername)
        {
            correctUsername = accounts.Find(x => x.Password == password).
                              Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault().Username;

            if (correctUsername != null)
                return true;

            return false;
        }

        public static void Insert(Account data)
        {
            if (db != null)
                accounts.Insert(data);
        }

        public static void InsertList(List<Account> accountList)
        {
            if (db == null)
                return;
            accounts.InsertBulk(accountList);
        }

        public static void Initialize(string DBname)
        {
            db = new LiteDatabase(DBname + ".db");
            accounts = db.GetCollection<Account>("accounts");

        }
    }
}