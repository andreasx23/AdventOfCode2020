using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    public class Day9Part2
    {
        private List<long> input = new List<long>();

        private void Day9()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long numberToFind = 507622668;
            int i = 0, j = 0;
            long sum = 0;            
            List<long> amountOfNums = new List<long>();
            while (sum != numberToFind)
            {
                var num = input[j];
                sum += num;
                j++;
                amountOfNums.Add(num);

                if (sum > numberToFind)
                {
                    i++;
                    j = i;
                    sum = 0;
                    amountOfNums.Clear();
                }
            }
            long ans = amountOfNums.Min() + amountOfNums.Max();
            watch.Stop();

            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 9\input.txt";
            input = File.ReadAllLines(path).Select(s => long.Parse(s)).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
