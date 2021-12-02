using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day02
{
    public class Day2Part1
    {
        private List<string> _instructions = new List<string>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int x = 0, y = 0;
            foreach (var instruction in _instructions)
            {
                var split = instruction.Split(' ');
                var direction = split.First();
                var amount = int.Parse(split.Last());
                switch (direction)
                {
                    case "forward":
                        y += amount;
                        break;
                    case "down":
                        x += amount;
                        break;
                    case "up":
                        x -= amount;
                        break;
                    default:
                        throw new Exception("Invalid");
                }
            }

            int ans = x * y;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day02\input.txt";
            _instructions = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
