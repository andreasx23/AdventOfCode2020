using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day01
{
    public class Day1Part1
    {
        private string input = string.Empty;

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var c in input)
            {
                if (c == '(')
                {
                    ans++;
                }
                else
                {
                    ans--;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day01\input.txt";
            input = File.ReadAllLines(path).First();
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
