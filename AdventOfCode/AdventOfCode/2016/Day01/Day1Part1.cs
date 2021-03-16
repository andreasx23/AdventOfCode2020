using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day01
{
    public class Day1Part1
    {
        private readonly List<(char direction, int amount)> input = new List<(char direction, int amount)>();

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int north = 0, east = 0;
            int dir = 0;
            foreach (var (direction, amount) in input)
            {
                if (direction == 'R')
                {
                    dir++;
                }
                else
                {
                    dir--;
                }

                if (dir < 0)
                {
                    dir = 3;
                }
                else if (dir > 3)
                {
                    dir = 0;
                }

                switch (dir)
                {
                    case 0:
                        north += amount;
                        break;
                    case 1:
                        east += amount;
                        break;
                    case 2:
                        north -= amount;
                        break;
                    case 3:
                        east -= amount;
                        break;
                    default:
                        break;
                }
            }

            int ans = Math.Abs(north) + Math.Abs(east);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day01\input.txt";
            var lines = File.ReadAllLines(path)[0].Split(',').Select(s => s.Trim()).ToList();

            foreach (var instructions in lines)
            {
                Console.WriteLine(instructions);

                char dir = instructions[0];
                int amount = Convert.ToInt32(instructions.Substring(1));
                input.Add((dir, amount));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
