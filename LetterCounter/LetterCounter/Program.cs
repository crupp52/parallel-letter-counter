using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterCounter
{
    class Program
    {
        static ConcurrentDictionary<string, int> dict = new ConcurrentDictionary<string, int>();
        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            string content = File.ReadAllText("song_of_ice_and_fire.txt");
            stopwatch.Start();
            //foreach (var item in content)
            //{
            //    Add(item.ToString());
            //}
            Parallel.ForEach(content, x => Add(x.ToString()));
            stopwatch.Stop();

            ShowResult();
        }

        static void Add(string c)
        {
            dict.AddOrUpdate(c, 1, (k, a) => a + 1);
        }

        static void ShowResult()
        {
            var r = (from e in dict
                    orderby e.Value descending
                    select e).Take(10);

            foreach (var item in r)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
            Console.WriteLine("Eltelt idő: {0}", stopwatch.Elapsed);
        }
    }
}
