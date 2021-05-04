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
        private string target = string.Empty;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int runs = 0;
            Queue<(string current, int count)> queue = new Queue<(string current, int count)>();
            foreach (var (from, to) in transistions.Where(kv => kv.from == "e"))
            {
                queue.Enqueue((to, 1));
                runs++;
            }

            int ans = 0;
            HashSet<string> isVisisted = new HashSet<string>();
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.current == target)
                {
                    ans = current.count;
                    break;
                }

                if (current.current.Length >= target.Length) continue;

                if (runs % 10000 == 0) Console.WriteLine(current.count);

                foreach (var (from, to) in transistions)
                {
                    for (int i = 0; i < current.current.Length; i++)
                    {
                        string temp = current.current[i].ToString();

                        if (from.Length == 2 && i > 0) temp = current.current[i - 1].ToString() + current.current[i].ToString();

                        if (temp == from)
                        {
                            string insert = (from.Length == 2) ?
                                current.current.Insert(i + 1, to).Remove(i - 1, 2) :
                                current.current.Insert(i + 1, to).Remove(i, 1);

                            if (isVisisted.Add(insert))
                            {
                                queue.Enqueue((insert, current.count + 1));
                            }
                        }
                    }
                }

                runs++;
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

            target = lines.Last();
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
