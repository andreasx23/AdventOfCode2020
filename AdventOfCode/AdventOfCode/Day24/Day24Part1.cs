using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    public class Day24Part1
    {
        private List<string> input = new List<string>();
        private readonly HashSet<(int, int)> visited = new HashSet<(int, int)>();
        private readonly Dictionary<string, (int, int)> dirs = new Dictionary<string, (int, int)>()
        {
            { "ne", (0, 1) },
            { "sw", (0, -1) },
            { "e", (1, 0) },
            { "w", (-1, 0) },
            { "nw", (-1, 1) },
            { "se", (1, -1) }
        }; 

        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var s in input)
            {
                int i = 0, j = 0;
                //Console.WriteLine(s);

                for (int k = 0; k < s.Length; k++)
                {
                    StringBuilder sb = new StringBuilder();
                    char c = s[k];
                    sb.Append(c);
                    if (c == 's' || c == 'n')
                    {
                        k++;
                        c = s[k];
                        sb.Append(c);
                    }

                    string dir = sb.ToString();
                    var tuple = dirs[dir];
                    i += tuple.Item1;
                    j += tuple.Item2;
                }

                if (!visited.Contains((i, j)))
                {
                    visited.Add((i, j));
                    //Console.WriteLine("New black tile");
                }
                else
                {
                    visited.Remove((i, j));
                    //Console.WriteLine("Turning black tile to white");
                }
            }
            int ans = visited.Count;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 24\sample.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
