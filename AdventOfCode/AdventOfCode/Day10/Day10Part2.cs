using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    public class Day10Part2
    {
        private List<int> input = new List<int>();

        private void Day10()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            input.Insert(0, 0);
            input.Add(input.Last() + 3);

            int n = input.Count;
            long[] dp = new long[n];
            dp[0] = 1;
            for (int i = 1; i < n; i++)
            {
                dp[i] = dp[i - 1];
                if (i > 1 && input[i] - input[i - 2] <= 3)
                {
                    dp[i] += dp[i - 2];
                }

                if (i > 2 && input[i] - input[i - 3] <= 3)
                {
                    dp[i] += dp[i - 3];
                }
            }
            long ans = dp.Last();
            watch.Stop();

            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 10\input.txt";
            input = File.ReadAllLines(path).Select(s => int.Parse(s)).OrderBy(n => n).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day10();
        }
    }
}
