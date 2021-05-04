using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day19
{
    public class Day19Part1
    {
        private List<(string from, string to)> transistions = new List<(string from, string to)>();
        private string input = string.Empty;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            HashSet<string> isVisited = new HashSet<string>();
            foreach (var (from, to) in transistions)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char current = input[i];

                    string temp;
                    if (from.Length == 2 && i > 0)
                    {
                        char left = input[i - 1];
                        temp = left.ToString() + current.ToString();
                    }
                    else
                    {
                        temp = current.ToString();
                    }

                    if (temp == from)
                    {
                        string insert = (from.Length == 2) ? input.Insert(i + 1, to).Remove(i - 1, 2) : input.Insert(i + 1, to).Remove(i, 1);
                        isVisited.Add(insert);
                    }
                }
            }

            int ans = isVisited.Count;

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
