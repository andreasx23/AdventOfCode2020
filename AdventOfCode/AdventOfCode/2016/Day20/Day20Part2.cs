using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day20
{
    public class Day20Part2
    {
        private List<(long min, long max)> input = new List<(long min, long max)>();

        private void Day20()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long currentMin = input[0].min, currentMax = input[0].max;
            List<(long lower, long upper)> blacklistedIpRanges = new List<(long lower, long upper)>();
            for (int i = 1; i < input.Count; i++)
            {
                (long min, long max) = input[i];

                if (currentMin < min && min < currentMax || min == currentMax + 1)
                {
                    currentMax = Math.Max(currentMax, max);
                }
                else
                {
                    blacklistedIpRanges.Add((currentMin, currentMax));
                    currentMin = min;
                    currentMax = max;
                }
            }

            long ans = 1;
            for (int i = 1; i < blacklistedIpRanges.Count; i++)
            {
                long min = blacklistedIpRanges[i - 1].upper, max = blacklistedIpRanges[i].lower;
                ans += max - (min + 1);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day20\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                long[] split = s.Split('-').Select(long.Parse).ToArray();
                input.Add((split[0], split[1]));
            }
            input = input.OrderBy(kv => kv.min).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day20();
        }
    }
}
