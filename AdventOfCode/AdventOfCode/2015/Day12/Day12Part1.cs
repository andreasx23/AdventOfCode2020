using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day12
{
    public class Day12Part1
    {
        private string input = string.Empty;

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string temp = "";
            int ans = 0;
            foreach (var c in input)
            {
                if (c == '-' || char.IsDigit(c))
                {
                    temp += c.ToString();
                }
                else if (!string.IsNullOrEmpty(temp))
                {
                    ans += int.Parse(temp);
                    temp = "";
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day12\input.txt";
            input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
