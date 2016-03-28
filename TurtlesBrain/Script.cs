using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurtlesBrain
{
    public class first
    {//first, suschstinkt, tester, rave1000_1, myTurtle

        Turtle turtle;
        
        public first(string label)
        {
            while (turtle == null)
            {
                Program.server.turtles.TryGetValue(label, out turtle);
                Thread.Sleep(500);
            }
        }

        public void doThis()
        {
            string reason;
            turtle.digDown(out reason);
            Console.WriteLine(reason);
        }

    }

    public class WriteText
    {
        List<Turtle> writerTurtles = new List<Turtle>();
        Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
        static string Text;
        List<Thread> t = new List<Thread>();
        int counter = 0;

        public WriteText(string[] label, string text)
        {

            Turtle turtle;
            for (int i = 0; i <= 4; i++)
            {
                turtle = null;
                while (turtle == null)
                {
                    Program.server.turtles.TryGetValue(label[i], out turtle);
                    Thread.Sleep(500);
                }
                writerTurtles.Add(turtle);
            }
            Text = text.ToLower();
            SortTurtles();
            Fill();
            t.Add(new Thread(writeLetter));
            t.Add(new Thread(writeLetter1));
            t.Add(new Thread(writeLetter2));
            t.Add(new Thread(writeLetter3));
            t.Add(new Thread(writeLetter4));
            t[0].Start();
            t[1].Start();
            t[2].Start();
            t[3].Start();
            t[4].Start();

        }

        public void SortTurtles()
        {
            List<Turtle> sorted = new List<Turtle>();
            List<Turtle> already = new List<Turtle>();
            already.AddRange(writerTurtles);
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
            writerTurtles = new List<Turtle>();
            writerTurtles.AddRange(sorted);
            while (!writerTurtles[4].down()) { }
            while (!writerTurtles[3].down()) { }
            while (!writerTurtles[2].down()) { }
            while (!writerTurtles[1].down()) { }
            while (!writerTurtles[0].down()) { }
        }

        private void writeLetter()
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[0].ToArray())
                {
                    if (item.ToString() == "-")
                        writerTurtles[0].place();
                    string reason;
                    writerTurtles[0].back(out reason);
                }
                writerTurtles[0].back();
                //    Interlocked.Increment(ref counter);
                //    while (counter < 4)
                //    {
                //        Thread.Sleep(50);
                //    }
                //    lock (this)
                //        counter = 0;
            }
        }
        private void writeLetter1()
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[1].ToArray())
                {
                    if (item.ToString() == "-")
                        writerTurtles[1].place();
                    writerTurtles[1].back();
                }
                writerTurtles[1].back();
                //     Interlocked.Increment(ref counter);
                //     while (counter < 4)
                //     {
                //         Thread.Sleep(50);
                //     }
                //     lock (this)
                //         counter = 0;
            }
        }
        private void writeLetter2()
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[2].ToArray())
                {
                    if (item.ToString() == "-")
                        writerTurtles[2].place();
                    writerTurtles[2].back();
                }
                writerTurtles[2].back();
                //   Interlocked.Increment(ref counter);
                //   while (counter < 4)
                //   {
                //       Thread.Sleep(50);
                //   }
                //   lock (this)
                //       counter = 0;
            }
        }
        private void writeLetter3()
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[3].ToArray())
                {
                    if (item.ToString() == "-")
                        writerTurtles[3].place();
                    writerTurtles[3].back();
                }
                writerTurtles[3].back();
                //    Interlocked.Increment(ref counter);
                //    while (counter < 4)
                //    {
                //        Thread.Sleep(50);
                //    }
                //    lock (this)
                //        counter = 0;
            }
        }
        private void writeLetter4()
        {
            string[] o;
            foreach (char letter in Text.ToArray())
            {
                dict.TryGetValue(letter.ToString(), out o);
                foreach (char item in o[4].ToArray())
                {
                    if (item.ToString() == "-")
                        writerTurtles[4].place();
                    writerTurtles[4].back();
                }
                writerTurtles[4].back();
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
                while (!writerTurtles[0].forward()) { writerTurtles[0].dig(); }
            }
        }
        private void machweg1()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!writerTurtles[1].forward()) { writerTurtles[0].dig(); }
            }
        }
        private void machweg2()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!writerTurtles[2].forward()) { writerTurtles[0].dig(); }
            }
        }
        private void machweg3()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!writerTurtles[3].forward()) { writerTurtles[0].dig(); }
            }
        }
        private void machweg4()
        {
            for (int i = 0; i < 30; i++)
            {
                while (!writerTurtles[4].forward()) { writerTurtles[0].dig(); }
            }
        }

        public void Fill()
        {
            dict.Add("a", new string[] { "---", "- -", "---", "- -", "- -" });
            dict.Add("h", new string[] { "-  -", "-  -", "----", "-  -", "-  -" });
            dict.Add("l", new string[] { "-  ", "-  ", "-  ", "-  ", "---" });
            dict.Add("m", new string[] { "-   -", "-- --", "- - -", "-   -", "-   -" });
            dict.Add("o", new string[] { "---", "- -", "- -", "- -", "---" });
            dict.Add("t", new string[] { "---", " - ", " - ", " - ", " - " });
            dict.Add(" ", new string[] { "  ", "  ", "  ", "  ", "  " });
        }
    }

}
//4Kp13tuj
