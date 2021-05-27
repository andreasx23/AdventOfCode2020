using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day16
{
    public class Day16Part2
    {
        private int[] input;

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<int> basePattern = new List<int>() { 0, 1, 0, -1 };

            int n = input.Length;
            int offset = int.Parse(string.Join("", input.Take(7)));
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

                //for (int j = 0; j < n; j++)
                //{
                //    int index = (j == 0) ? 1 : 0;
                //    int count = (j == 0) ? 0 : 1;
                //    int sum = 0;
                //    for (int k = 0; k < n; k++)
                //    {
                //        int val = input[k];
                //        int times = basePattern[index];
                //        int calc = val * times;
                //        sum += calc;

                //        if (++count == j + 1)
                //        {
                //            count = 0;
                //            index = (index + 1) % basePattern.Count;
                //        }
                //    }
                //    temp[j] = int.Parse(sum.ToString().Last().ToString());
                //}
                input = temp;
                Console.WriteLine(i);
            }

            //string ans = string.Join("", input.Take(8));
            string ans = string.Join("", input.Skip(offset).Take(8));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<int> GeneratePattern(int repeat)
        {
            List<int> basePattern = new List<int>() { 0, 1, 0, -1 };
            List<int> pattern = new List<int>();

            for (int i = 0; i < basePattern.Count; i++)
            {
                for (int j = 0; j < repeat; j++)
                {
                    pattern.Add(basePattern[i]);
                }
            }

            return pattern;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day16\input.txt";
            var lines = File.ReadAllLines(path).First().ToArray().Select(c => int.Parse(c.ToString())).ToArray();

            int n = 10_000;
            input = new int[lines.Length * n];

            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    input[index++] = lines[j];
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
