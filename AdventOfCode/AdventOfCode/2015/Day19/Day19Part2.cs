using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day19
{
    public class Day19Part2
    {
        private readonly List<(string from, string to)> transistions = new List<(string from, string to)>();
        private string input = string.Empty;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string temp = input;
            int ans = 0, sameTurns = 0;
            Random rand = new Random();
            while (temp != "e")
            {
                var (from, to) = transistions[rand.Next(0, transistions.Count)];

                while (temp.Contains(to))
                {
                    var index = temp.IndexOf(to);
                    temp = temp.Remove(index, to.Length);
                    temp = temp.Insert(index, from);
                    ans++;
                }

                if (sameTurns == 1000)
                {
                    sameTurns = 0;
                    ans = 0;
                    temp = input;
                }
                else
                {
                    sameTurns++;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day19\input.txt";
            var lines = File.ReadAllLines(path);

            int i = 0;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var split = lines[i].Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                transistions.Add((split.First(), split.Last()));
                i++;
            }

            input = lines.Last();
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
