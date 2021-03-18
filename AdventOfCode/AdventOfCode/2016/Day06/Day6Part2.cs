using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day06
{
    public class Day6Part2
    {
        private List<string> input = new List<string>();

        private void Day6()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = string.Empty;
            for (int i = 0; i < input[0].Length; i++)
            {
                Dictionary<char, int> map = new Dictionary<char, int>();
                for (int j = 0; j < input.Count; j++)
                {
                    char c = input[j][i];
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c, 0);
                    }
                    map[c]++;
                }

                var order = map.OrderBy(kv => kv.Value).First();
                ans += order.Key;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day06\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
