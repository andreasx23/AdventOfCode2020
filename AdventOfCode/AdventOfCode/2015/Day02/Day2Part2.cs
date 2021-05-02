using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day02
{
    public class Day2Part2
    {
        private List<List<int>> inputs = new List<List<int>>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var values in inputs)
            {
                int ribbon = values[0] * 2 + values[1] * 2;
                int bow = values[0] * values[1] * values[2];
                ans += ribbon + bow;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day02\input.txt";
            inputs = File.ReadAllLines(path).Select(s => s.Split('x').Select(int.Parse).OrderBy(n => n).ToList()).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
