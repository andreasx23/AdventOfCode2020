using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13
{
    public class Day13Part1
    {
        private int departure = 0;
        private HashSet<int> set = new HashSet<int>();

        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            set.Remove(-1);

            int lowest = int.MaxValue, ans = 0;
            foreach (var num in set)
            {
                int currentSum = num, prevSum = -1;
                while (currentSum < departure)
                {
                    prevSum = currentSum;
                    currentSum += num;
                }

                if (currentSum == departure)
                {
                    lowest = currentSum;
                    ans = num;
                    break;
                }

                int sum = currentSum - departure;
                if (sum < lowest)
                {
                    lowest = sum;
                    ans = Math.Abs((departure - currentSum) * num);
                }
            }

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 13\input.txt";
            var input = File.ReadAllLines(path);

            departure = int.Parse(input[0]);
            set = input[1].Split(',').Select(s =>
            {
                if (s[0] != 'x')
                {
                    return int.Parse(s);
                }

                return -1;
            }).ToHashSet();
        }

        public void TestCase()
        {
            ReadData();
            Day13();
        }
    }
}
