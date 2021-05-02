using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day02
{
    public class Day2Part1
    {
        private List<List<int>> inputs = new List<List<int>>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var values in inputs)
            {
                int side1 = values[0] * values[1];
                int side2 = values[1] * values[2];
                int side3 = values[2] * values[0];
                int squareFeet = 2 * side1 + 2 * side2 + 2 * side3;
                ans += squareFeet + Math.Min(side1, Math.Min(side2, side3));
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day02\input.txt";
            inputs = File.ReadAllLines(path).Select(s => s.Split('x').Select(int.Parse).ToList()).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
