using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
{
    public class Day19Part1
    {
        private HashSet<string> input = new HashSet<string>();
        private List<string> messages = new List<string>();

        //Unsolved
        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 19\input.txt";
            var lines = File.ReadAllLines(path);

            int index = 0;
            Dictionary<int, List<string>> map = new Dictionary<int, List<string>>();
            while (!string.IsNullOrEmpty(lines[index]))
            {
                string s = lines[index];
                var split = s.Split(':');
                var rules = split[1].Trim().Split('|');

                map.Add(index, new List<string>());
                foreach (var _rule in rules)
                {
                    string rule = _rule.Trim();
                    map[index].Add(rule);
                }
                index++;
            }
            
            for (int i = index + 1; i < lines.Length; i++)
            {
                messages.Add(lines[i]);
            }

            var ordered = map.OrderBy(kv => kv.Key);
            foreach (var kv_map in map)
            {
                //Console.WriteLine(item.Key + " " + string.Join(" ", item.Value));
                StringBuilder sb = new StringBuilder();

                while (true)
                {

                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
