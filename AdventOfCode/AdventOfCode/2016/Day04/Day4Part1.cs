using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day04
{
    public class Day4Part1
    {
        private readonly Dictionary<string, string> map = new Dictionary<string, string>();

        //https://adventofcode.com/2016/day/4
        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var kv in map)
            {
                Dictionary<char, int> count = new Dictionary<char, int>();
                foreach (var c in kv.Key)
                {
                    if (c == '-')
                    {
                        continue;
                    }

                    if (!count.ContainsKey(c)) 
                    {
                        count.Add(c, 0);
                    }
                    count[c]++;
                }

                var order = count.OrderByDescending(kv => kv.Value).Take(5).Select(kv => kv.Key).ToList();

                bool isValid = true;
                for (int i = 0; i < 5; i++)
                {
                    if (kv.Value[i] != order[i])
                    {
                        Console.WriteLine(kv.Value[i] + " " + order[i]);
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    int id = int.Parse(kv.Key.Substring(kv.Key.Length - 3));
                    ans += id;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day04\sample.txt";
            var lines = File.ReadAllLines(path);

            //ide-htrgti-gpqqxi-rjhidbtg-htgkxrt-193[gynxm]
            foreach (var item in lines)
            {
                var key = item.Substring(0, item.Length - 7);
                var value = item.Substring(item.Length - 7);
                value = value.Substring(1, value.Length - 2);
                map.Add(key, value);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
