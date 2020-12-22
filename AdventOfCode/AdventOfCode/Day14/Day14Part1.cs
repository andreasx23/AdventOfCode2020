using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    public class Day14Part1
    {
        private Dictionary<string, List<Mem>> map = new Dictionary<string, List<Mem>>();

        public class Mem
        {
            public long id;
            public long value;
        }

        private void Day14()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Dictionary<long, long> bits = new Dictionary<long, long>();
            foreach (var kvp in map)
            {
                foreach (var mem in kvp.Value)
                {
                    char[] valueToBit = Convert.ToString(mem.value, 2).PadLeft(36, '0').ToCharArray();
                    for (int i = 0; i < valueToBit.Length; i++)
                    {
                        char c = kvp.Key[i];
                        if (c != 'X')
                        {
                            valueToBit[i] = c;
                        }
                    }

                    bits[mem.id] = Convert.ToInt64(new string(valueToBit), 2);
                }
            }
            long ans = bits.Values.Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 14\input.txt";
            var lines = File.ReadAllLines(path);

            string mask = string.Empty;
            foreach (var s in lines)
            {
                if (s.Contains("mask"))
                {
                    mask = s.Split(new string[] { "mask = " }, StringSplitOptions.None)[1].Trim();
                    map.Add(mask, new List<Mem>());
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    var splits = s.Split('=').Select(_s => _s.Trim()).ToList();

                    for (int i = 4; i < splits[0].Length - 1; i++)
                    {
                        char c = splits[0][i];
                        sb.Append(c);
                    }

                    long id = int.Parse(sb.ToString()), value = long.Parse(splits[1]);

                    map[mask].Add(new Mem()
                    {
                        id = id,
                        value = value
                    });
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day14();
        }
    }
}
