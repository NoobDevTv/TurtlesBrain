using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtlesBrain;
using LiteDB;
using System.Diagnostics;
using System.Threading;

namespace DBTeast
{
    class Program
    {

        static LiteDatabase db = new LiteDatabase(@"Book.db");
        static LiteCollection<Book> books = db.GetCollection<Book>("Books");
        static void Main(string[] args)
        {

            //for (int i = 0; i < 100000; i++)
            //{
            //

            //}
            //var o = stop.Elapsed.Milliseconds;
            //books.EnsureIndex(x => x.Author);
            //var res = books.Find(x => x.Id > 0).Where(x => x.Id % 157 == 0);
            //
            //foreach (Book item in res)
            //{
            //    Console.WriteLine(item.Id + " " + item.Name);
            //}
            List<Book> BookList = new List<Book>();
            for (int i = 0; i < 100000; i++)
            {
                //books.Insert(book);
                BookList.Add(new Book { Name = $"Jesus {i}", Author = "Noone", Id = i++ });
            }
            books.InsertBulk(BookList);
            Console.ReadKey();

            /*
             DB db = new DB("server=(local);password=;options=none;");
             if (!File.Exists("ourDB.eq"))
                 db.CreateDatabase("ourDB");

             db.OpenDatabase("ourDB");

             
             db.Store(new Account("susch", "thisisapasswortfortheturtle"));

            db.Close();

            Console.ReadKey();*/
        }

        public static void FuegeHinzu(object o)
        {

        }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

}
