using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day15
{
    public class Day15Part2
    {
        private readonly List<List<(string name, int amount)>> input = new List<List<(string name, int amount)>>();
        private const int teaSpoons = 100;

        private void Day15()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long ans = long.MinValue;
            for (int i = 0; i <= teaSpoons; i++)
            {
                for (int j = 0; j <= teaSpoons - i; j++)
                {
                    for (int k = 0; k <= teaSpoons - i - j; k++)
                    {
                        for (int x = 0; x <= teaSpoons - i - j - k; x++)
                        {
                            long capacity = 0, durability = 0, flavour = 0, texture = 0, calories = 0;

                            capacity += input[0][0].amount * i;
                            durability += input[0][1].amount * i;
                            flavour += input[0][2].amount * i;
                            texture += input[0][3].amount * i;
                            calories += input[0][4].amount * i;

                            capacity += input[1][0].amount * j;
                            durability += input[1][1].amount * j;
                            flavour += input[1][2].amount * j;
                            texture += input[1][3].amount * j;
                            calories += input[1][4].amount * j;

                            capacity += input[2][0].amount * k;
                            durability += input[2][1].amount * k;
                            flavour += input[2][2].amount * k;
                            texture += input[2][3].amount * k;
                            calories += input[2][4].amount * k;

                            capacity += input[3][0].amount * x;
                            durability += input[3][1].amount * x;
                            flavour += input[3][2].amount * x;
                            texture += input[3][3].amount * x;
                            calories += input[3][4].amount * x;

                            if (calories == 500)
                            {
                                long sum = Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavour) * Math.Max(0, texture);
                                ans = Math.Max(ans, sum);
                            }
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day15\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var remaining = s.Substring(s.IndexOf(':') + 2);
                var items = remaining.Split(',').Select(_ => _.Trim().Split(' ').ToList()).ToList();
                List<(string, int)> values = new List<(string, int)>();
                foreach (var item in items)
                {
                    values.Add((item.First(), int.Parse(item.Last())));
                }
                input.Add(values);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day15();
        }
    }
}
