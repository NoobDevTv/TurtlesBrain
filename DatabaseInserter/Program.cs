using System;
using LiteDB;

namespace DatabaseInserter
{
    class Program
    {
        static LiteDatabase db;
        static LiteCollection<Account> accounts;
        static void Main(string[] args)
        {
            Console.Write("Name of database: ");
            string s = Console.ReadLine();
            if (!s.Contains(".db"))
                s += ".db";
            db = new LiteDatabase(s);
            accounts = db.GetCollection<Account>("accounts");
            Account account = new Account();
            Console.Write("Username: ");
            account.Username = Console.ReadLine();
            Console.Write("Passwort: ");
            account.Password = Console.ReadLine();
            accounts.Insert(account);
            Console.WriteLine("Successfully inserted. Press any key to close this console");
            Console.ReadKey();
        }

    }
}
