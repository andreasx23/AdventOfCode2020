using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day20
{
    public class Day20Part1
    {
        private readonly int input = 29_000_000;
        private readonly int amount = 1_000_001;

        private void Day20()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int[] houses = new int[amount + 1];
            int ans = int.MaxValue;
            Parallel.For(1, amount, elf =>
            {
                for (int i = elf; i <= amount; i += elf)
                {
                    houses[i] += elf * 10;

                    if (houses[i] >= input)
                    {
                        ans = Math.Min(ans, i);
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
