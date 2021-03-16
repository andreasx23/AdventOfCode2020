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

        //https://adventofcode.com/2016/day/1
        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string dirs = "NESW";
            int east = 0, north = 0;
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

                if (dir == 0)
                {
                    north += amount;
                }
                else if (dir == 1)
                {
                    east += amount;
                }
                else if (dir == 2)
                {
                    north -= amount;
                }
                else
                {
                    east -= amount;
                }

                Console.WriteLine(east + " " + north);
            }

            Console.WriteLine(east + " " + north);

            int ans = east + north;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day01\sample.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(',').Select(s => s.Trim())).ToList();

            foreach (var instructions in lines[0])
            {
                char dir = instructions[0];
                int amount = Convert.ToInt32(instructions[1].ToString());
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
