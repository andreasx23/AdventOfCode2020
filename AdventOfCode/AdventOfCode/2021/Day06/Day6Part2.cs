using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day06
{
    public class Day6Part2
    {
        private Dictionary<long, long> _map = new Dictionary<long, long>();

        private void Day6()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int cycles = 256;
            for (int i = 0; i < cycles; i++)
            {
                Dictionary<long, long> map = new Dictionary<long, long>();
                foreach (var kv in _map)
                {
                    if (kv.Key == 0)
                    {
                        if (!map.ContainsKey(6))
                            map.Add(6, 0);
                        map[6] += kv.Value;
                        if (!map.ContainsKey(8))
                            map.Add(8, 0);
                        map[8] += kv.Value;
                    }
                    else
                    {
                        if (!map.ContainsKey(kv.Key - 1))
                            map.Add(kv.Key - 1, 0);
                        map[kv.Key - 1] += kv.Value;
                    }
                }
                _map = map;
                Console.WriteLine("Day: " + (i + 1) + " done");
            }

            long ans = _map.Values.Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day06\input.txt";
            var input = File.ReadAllLines(path).First().Split(',').Select(long.Parse).ToList();
            foreach (var n in input)
            {
                if (!_map.ContainsKey(n))
                    _map.Add(n, 0);
                _map[n]++;
            }
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
