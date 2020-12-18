using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15
{
    public class Day15Part1
    {
        private List<int> input = new List<int>();
        private readonly Dictionary<int, int> map = new Dictionary<int, int>();

        private void Day15()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int turn = 0;
            foreach (var num in input)
            {
                map.Add(num, ++turn); //Key = num -- value = turn spoken
            }

            int prevSpoken = input.Last();
            for (int i = input.Count; i < 2020; i++)
            {
                if (map.ContainsKey(prevSpoken))
                {
                    var temp = i - map[prevSpoken];
                    map[prevSpoken] = i;
                    prevSpoken = temp;
                }
                else
                {
                    map.Add(prevSpoken, i);
                    prevSpoken = 0;
                }
            }
            int ans = prevSpoken;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 15\sample.txt";
            input = File.ReadAllLines(path)[0].Split(',').Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day15();
        }
    }
}
