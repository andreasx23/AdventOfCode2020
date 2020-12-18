using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day16
{
    public class Day16Part1
    {
        private readonly List<Rule> rules = new List<Rule>();
        private List<int> myTicket = new List<int>();
        private readonly Dictionary<int, List<int>> nearbyTickets = new Dictionary<int, List<int>>();

        public class Rule
        {
            public string name;
            public int lowerMin;
            public int lowerMax;
            public int higherMin;
            public int higherMax;

            public bool IsMatch(int value)
            {
                if (value >= lowerMin && value <= lowerMax || value >= higherMin && value <= higherMax)
                {
                    return true;
                }

                return false;
            }
        }

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var ticketValues in nearbyTickets.Values) //Each person with a nearby ticket
            {
                foreach (var value in ticketValues)
                {
                    bool isMatch = false;
                    foreach (var rule in rules)
                    {
                        if (rule.IsMatch(value))
                        {
                            isMatch = true;
                            break;
                        }                       
                    }

                    if (!isMatch)
                    {
                        ans += value;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 16\input.txt";
            var lines = File.ReadAllLines(path);

            int counter = 0;
            while (!string.IsNullOrEmpty(lines[counter]))
            {
                var splits = lines[counter].Split(' ');

                var name = $"{splits[0]} {(!char.IsDigit(splits[1][0]) ? splits[1] : "")}".Trim();
                if (name.Last() == ':')
                {
                    var toRemove = name.Length - 1;
                    name = name.Remove(toRemove);
                }

                Rule rule = new Rule
                {
                    name = name
                };

                bool isLower = true;
                foreach (var s in splits)
                {
                    if (s.Contains("-"))
                    {
                        string[] split = s.Split('-');
                        int min = int.Parse(split[0]), max = int.Parse(split[1]);

                        if (isLower)
                        {
                            isLower = false;
                            rule.lowerMin = min;
                            rule.lowerMax = max;                            
                        }
                        else
                        {
                            rule.higherMin = min;
                            rule.higherMax = max;                            
                        }                        
                    }
                }
                rules.Add(rule);
                counter++;
            }
            counter += 2;

            myTicket = lines[counter].Split(',').Select(int.Parse).ToList();

            counter += 3;
            int index = 1;
            for (int i = counter; i < lines.Length; i++)
            {
                nearbyTickets.Add(index, new List<int>());
                nearbyTickets[index] = lines[i].Split(',').Select(int.Parse).ToList();
                index++;
            }
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
