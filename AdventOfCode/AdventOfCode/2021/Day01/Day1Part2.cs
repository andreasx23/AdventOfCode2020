using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day01
{
    public class Day1Part2
    {
        private List<int> _depths = new List<int>();

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            int prevPrev = _depths.First();
            int prev = _depths[1];
            int prevSum = int.MaxValue;
            for (int i = 2; i < _depths.Count; i++)
            {
                int current = _depths[i];
                int sum = prevPrev + prev + current;
                if (sum > prevSum) ans++;
                prevPrev = prev;
                prev = current;
                prevSum = sum;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day01\input.txt";
            _depths = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
