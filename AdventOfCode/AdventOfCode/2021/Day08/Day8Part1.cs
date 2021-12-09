using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day08
{
    public class Day8Part1
    {
        private readonly List<(List<string> left, List<string> right)> _parts = new List<(List<string> left, List<string> right)>();

        private void Day8()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int ans = 0;
            foreach (var (left, right) in _parts)
                ans += right.Count(s => s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day08\input.txt";
            var input = File.ReadAllLines(path).Select(s => s.Split('|').Select(s => s.Split(' ').ToList()).ToList()).ToList();
            foreach (var item in input)
                _parts.Add((item.First(), item.Last()));
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
