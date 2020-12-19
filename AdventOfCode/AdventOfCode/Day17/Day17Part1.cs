using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17
{
    public class Day17Part1
    {
        private List<string> input = new List<string>();

        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Console.WriteLine("TODO");
            int ans = 0;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\Day 17\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day17();
        }
    }
}
