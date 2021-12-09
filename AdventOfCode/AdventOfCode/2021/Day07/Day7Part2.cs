using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day07
{
    public class Day7Part2
    {
        private class Crab
        {
            public int InitialPosition { get; set; }
        }

        private readonly List<Crab> _crabs = new List<Crab>();

        private void Day7()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int ans = int.MaxValue;
            int min = _crabs.Min(c => c.InitialPosition), max = _crabs.Max(c => c.InitialPosition);
            for (int i = min; i <= max; i++)
            {
                int sum = 0;
                foreach (var crab in _crabs)
                {
                    int n = Math.Abs(i - crab.InitialPosition);
                    int cost = n * (n + 1) / 2;
                    sum += cost;
                }
                ans = Math.Min(ans, sum);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day07\input.txt";
            var input = File.ReadAllLines(path).First().Split(',').Select(int.Parse).ToList();
            foreach (var item in input)
            {
                _crabs.Add(new Crab() { InitialPosition = item });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
