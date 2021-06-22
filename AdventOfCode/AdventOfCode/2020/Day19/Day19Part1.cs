using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
{
    public class Day19Part1
    {
        class Rule
        {
            public int Id;
            public List<List<Rule>> Children = new List<List<Rule>>();
            public char Value;
        }

        private readonly Dictionary<int, Rule> map = new Dictionary<int, Rule>();
        private readonly List<string> words = new List<string>();

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Rule> rules = map.Values.ToList();
            Rule zero = rules.First(r => r.Id == 0);
            string pattern = GenerateRegexPattern(zero);
            int ans = words.Count(w => Regex.IsMatch(w, pattern));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string GenerateRegexPattern(Rule current)
        {
            if (char.IsLetter(current.Value))
            {
                return current.Value.ToString();
            }

            string part = "";
            foreach (var childs in current.Children)
            {
                string innerPart = "";
                foreach (var child in childs)
                {
                    innerPart += GenerateRegexPattern(child);
                }
                part += $"({innerPart})";
            }
            return part;
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2020\Day 19\sample.txt";
            var lines = File.ReadAllLines(path);

            int index = 0;
            while (!string.IsNullOrEmpty(lines[index]))
            {
                var split = lines[index].Split(':').Select(s => s.Trim());

                if (!map.ContainsKey(int.Parse(split.First())))
                {
                    map.Add(int.Parse(split.First()), new Rule() { Id = int.Parse(split.First()) });
                }

                var current = map[int.Parse(split.First())];

                var children = split.Last().Split('|').Select(s => s.Trim());
                foreach (var s in children)
                {
                    if (s.Any(c => char.IsLetter(c)))
                    {
                        current.Value = s.Substring(1, 1).First();
                        break;
                    }

                    var nums = s.Split(' ').Select(val => int.Parse(val.Trim()));

                    List<Rule> rules = new List<Rule>();
                    foreach (var num in nums)
                    {
                        if (!map.ContainsKey(num))
                        {
                            map.Add(num, new Rule() { Id = num });
                        }
                        var child = map[num];
                        rules.Add(child);
                    }
                    current.Children.Add(rules);
                }

                index++;
            }

            index++;
            for (int i = index; i < lines.Length; i++)
            {
                words.Add(lines[i]);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
