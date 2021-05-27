using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day16
{
    public class Day16Part1
    {
        private int[] input;

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<int> basePattern = new List<int>() { 0, 1, 0, -1 };

            int n = input.Length;
            for (int i = 0; i < 100; i++)
            {
                int[] temp = new int[n];

                Parallel.For(0, n, j =>
                {
                    int index = (j == 0) ? 1 : 0;
                    int count = (j == 0) ? 0 : 1;
                    int sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        int val = input[k];
                        int times = basePattern[index];
                        int calc = val * times;
                        sum += calc;

                        if (++count == j + 1)
                        {
                            count = 0;
                            index = (index + 1) % basePattern.Count;
                        }
                    }
                    temp[j] = int.Parse(sum.ToString().Last().ToString());
                });

                input = temp;
            }

            string ans = string.Join("", input.Take(8));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day16\input.txt";
            input = File.ReadAllLines(path).First().ToArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
