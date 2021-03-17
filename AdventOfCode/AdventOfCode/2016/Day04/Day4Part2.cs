using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day04
{
    public class Day4Part2
    {
        private readonly Dictionary<string, int> map = new Dictionary<string, int>();

        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<char> alphabet = new List<char>();
            for (char i = 'a'; i <= 'z'; i++)
            {
                alphabet.Add(i);
            }

            int ans = 0;
            foreach (var kv in map)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var c in kv.Key)
                {
                    if (c == '-')
                    {
                        sb.Append(' ');
                    }
                    else
                    {
                        int index = (alphabet.IndexOf(c) + kv.Value) % 26;
                        sb.Append(alphabet[index]);
                    }
                }

                if (sb.ToString().ToLower().Contains("north"))
                {
                    ans = kv.Value;
                    break;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day04\input.txt";
            var lines = File.ReadAllLines(path);

            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (var s in lines)
            {
                var key = s.Substring(0, s.Length - 7);
                var value = s.Substring(s.Length - 7);
                value = value.Substring(1, value.Length - 2);
                temp.Add(key, value);
            }

            foreach (var kv in temp)
            {
                Dictionary<char, int> count = new Dictionary<char, int>();
                foreach (var c in kv.Key)
                {
                    if (c == '-' || char.IsDigit(c))
                    {
                        continue;
                    }

                    if (!count.ContainsKey(c))
                    {
                        count.Add(c, 0);
                    }
                    count[c]++;
                }

                var order = count.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(5).Select(kv => kv.Key).ToList();

                bool isValid = true;
                for (int i = 0; i < 5; i++)
                {
                    if (kv.Value[i] != order[i])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    int id = int.Parse(kv.Key.Substring(kv.Key.Length - 3));
                    map.Add(kv.Key.Substring(0, kv.Key.Length - 4), id);
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
