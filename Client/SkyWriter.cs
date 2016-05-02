using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using turtleAPI;
using static System.Configuration.ConfigurationManager;

namespace Client
{
    public class SkyWriter
    {
        List<Turtle> skyWriter = new List<Turtle>();
        Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
        static string Text;
        List<Thread> t = new List<Thread>();
        public SkyWriter(string[] label, string text)
        {
            var server = Server.Connect(AppSettings.Get("ServerIp"),Convert.ToInt32(AppSettings.Get("ServerPort")), 
                AppSettings.Get("Username"), AppSettings.Get("Password"));
            Console.WriteLine(DateTime.UtcNow.Ticks);
            ThreadPool.SetMaxThreads(50, 50);
            Task.Run(() => Console.WriteLine(DateTime.UtcNow.Ticks));
            Console.ReadKey();
            //////////////* skyWriter.Add(server[label[0]]);*/
            //skyWriter.AddRange(label.Select(l => server[l]));
            Console.WriteLine("STARTING");
            Text = text.ToLower();
            //  Console.ReadKey();
            skyWriter.AddRange(server.AllTurtles);
            for (int i = 0; i < skyWriter.Count; i++)
            {
                t.Add(new Thread(Turner));
                t[i].Start(i);
            }
            Console.ReadKey();
            //SortTurtles();
            //Fill();

            //t.Add(new Thread(writeLetter));
            //t.Add(new Thread(writeLetter1));
            //t.Add(new Thread(writeLetter2));
            //t.Add(new Thread(writeLetter3));
            //t.Add(new Thread(writeLetter4));

            //t.Add(new Thread(Turner));
            //t.Add(new Thread(Turner));
            //t.Add(new Thread(Turner));
            //t.Add(new Thread(Turner));
            //t.Add(new Thread(Turner));
            //t[0].Start(0);
            //t[1].Start(1);
            //t[2].Start(2);
            //t[3].Start(3);
            //t[4].Start(4);
            //
        }

        public void Turner(object o)
        {
            var turtle = skyWriter[(int)o];
            while (true)
            {
                turtle.turnRight();
            }
        }

        public void SortTurtles()
        {
            List<Turtle> sorted = new List<Turtle>();
            List<Turtle> already = new List<Turtle>();
            already.AddRange(skyWriter);
            for (int i = 0; i <= 4; i++)
            {
                foreach (Turtle item in already)
                {
                    if (!item.up())
                        continue;
                    already.Remove(item);
                    sorted.Add(item);
                    break;
                }
            }
            skyWriter = new List<Turtle>();
            skyWriter.AddRange(sorted);
            while (!skyWriter[4].down()) { }
            while (!skyWriter[3].down()) { }
            while (!skyWriter[2].down()) { }
            while (!skyWriter[1].down()) { }
            while (!skyWriter[0].down()) { }
        }

        private void writeLetter(object f)
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[0].ToArray())
                {
                    if (item.ToString() == "-")
                        skyWriter[0].place();
                    string reason;
                    skyWriter[0].back(out reason);
                }
                skyWriter[0].back();
                //    Interlocked.Increment(ref counter);
                //    while (counter < 4)
                //    {
                //        Thread.Sleep(50);
                //    }
                //    lock (this)
                //        counter = 0;
            }
        }
        private void writeLetter1(object f)
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[1].ToArray())
                {
                    if (item.ToString() == "-")
                        skyWriter[1].place();
                    skyWriter[1].back();
                }
                skyWriter[1].back();
                //     Interlocked.Increment(ref counter);
                //     while (counter < 4)
                //     {
                //         Thread.Sleep(50);
                //     }
                //     lock (this)
                //         counter = 0;
            }
        }
        private void writeLetter2(object f)
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[2].ToArray())
                {
                    if (item.ToString() == "-")
                        skyWriter[2].place();
                    skyWriter[2].back();
                }
                skyWriter[2].back();
                //   Interlocked.Increment(ref counter);
                //   while (counter < 4)
                //   {
                //       Thread.Sleep(50);
                //   }
                //   lock (this)
                //       counter = 0;
            }
        }
        private void writeLetter3(object f)
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[3].ToArray())
                {
                    if (item.ToString() == "-")
                        skyWriter[3].place();
                    skyWriter[3].back();
                }
                skyWriter[3].back();
                //    Interlocked.Increment(ref counter);
                //    while (counter < 4)
                //    {
                //        Thread.Sleep(50);
                //    }
                //    lock (this)
                //        counter = 0;
            }
        }
        private void writeLetter4(object f)
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[4].ToArray())
                {
                    if (item.ToString() == "-")
                        skyWriter[4].place();
                    skyWriter[4].back();
                }
                skyWriter[4].back();
                //   Interlocked.Increment(ref counter);
                //   while (counter < 4)
                //   {
                //       Thread.Sleep(50);
                //   }
                //   lock (this)
                //       counter = 0;
            }
        }

        private void machweg()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!skyWriter[0].forward()) { skyWriter[0].dig(); }
            }
        }
        private void machweg1()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!skyWriter[1].forward()) { skyWriter[0].dig(); }
            }
        }
        private void machweg2()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!skyWriter[2].forward()) { skyWriter[0].dig(); }
            }
        }
        private void machweg3()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!skyWriter[3].forward()) { skyWriter[0].dig(); }
            }
        }
        private void machweg4()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!skyWriter[4].forward()) { skyWriter[0].dig(); }
            }
        }

        public void Fill()
        {
            dict.Add("a", new string[] { "---", "- -", "---", "- -", "- -" });
            dict.Add("b", new string[] { "---", "- -", "---", "- -", "---" });
            dict.Add("c", new string[] { "---", "-  ", "-  ", "-  ", "---" });
            dict.Add("d", new string[] { "-- ", "- -", "- -", "- -", "-- " });
            dict.Add("e", new string[] { "---", "-  ", "-- ", "-  ", "---" });
            dict.Add("f", new string[] { "---", "-  ", "-- ", "-  ", "-  " });
            dict.Add("g", new string[] { "---", "-  ", "- -", "- -", "---" });
            dict.Add("h", new string[] { "-  -", "-  -", "----", "-  -", "-  -" });//suschstinkt
            dict.Add("i", new string[] { "-", "-", "-", "-", "-" });
            dict.Add("j", new string[] { "---", "  -", "  -", "- -", "---" });
            dict.Add("k", new string[] { "-  -", "- - ", "--  ", "- - ", "-  -" });
            dict.Add("l", new string[] { "-  ", "-  ", "-  ", "-  ", "---" });
            dict.Add("m", new string[] { "-   -", "-- --", "- - -", "-   -", "-   -" });
            dict.Add("n", new string[] { "-   ", "----", "-  -", "-  -", "-  -" });
            dict.Add("o", new string[] { "---", "- -", "- -", "- -", "---" });
            dict.Add("p", new string[] { "---", "- -", "---", "-  ", "-  " });
            dict.Add("r", new string[] { "---", "- -", "---", "-- ", "- -" });
            dict.Add("s", new string[] { "---", "-  ", "---", "  -", "---" });
            dict.Add("t", new string[] { "---", " - ", " - ", " - ", " - " });
            dict.Add("u", new string[] { "- -", "- -", "- -", "- -", "---" });
            dict.Add("v", new string[] { "-   -", "-   -", " - - ", " - - ", "  -  " });
            dict.Add("w", new string[] { "-   -", "-  - ", "- - -", "-- --", "-   -" });
            dict.Add("x", new string[] { "-   -", " - - ", "  -  ", " - - ", "-   -" });
            dict.Add("y", new string[] { "- -", "- -", " - ", " - ", " - " });
            dict.Add("z", new string[] { "-----", "   - ", "  -  ", " -   ", "-----" });
            dict.Add(" ", new string[] { "  ", "  ", "  ", "  ", "  " });
        }
    }

}
