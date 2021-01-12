using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day05
{
    public class Day5Part1
    {
        private List<int> input = new List<int>();

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2019\Day04\input.txt";
            input = File.ReadAllLines(path)[0].Split(',').Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
