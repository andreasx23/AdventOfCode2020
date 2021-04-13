using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day20
{
    public class Day20Part2
    {
        private readonly int input = 29_000_000;
        private static readonly int elvesAmount = 1_000_001;
        private readonly int houseAmount = elvesAmount * 51;

        private void Day20()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int[] houses = new int[houseAmount + 1];
            int ans = int.MaxValue;
            Parallel.For(1, elvesAmount, elf =>
            {
                for (int i = 1; i <= 50; i++)
                {
                    int index = i * elf;
                    houses[index] += elf * 11;

                    if (houses[index] >= input)
                    {
                        ans = Math.Min(ans, index);
                    }
                }
            });

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day21\Gear.txt";
            //var gearInformation = File.ReadAllLines(path);
        }

        public void TestCase()
        {
            ReadData();
            Day20();
        }
    }
}
