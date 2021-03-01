using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day01
{
    public class Day1Part1
    {
        List<int> input = new List<int>();

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long ans = input.Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day01\input.txt";
            input = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
