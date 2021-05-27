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

            Dictionary<int, List<int>> patterns = new Dictionary<int, List<int>>();
            int n = input.Length;

            for (int i = 1; i <= n; i++)
            {
                patterns.Add(i, GeneratePattern(i));
            }

            for (int i = 0; i < 100; i++)
            {
                int[] temp = new int[n];

                int _sum = input.Sum();
                for (int j = 1; j <= n; j++)
                {
                    var pattern = GeneratePattern(j);
                    var patternSum = pattern.Sum();

                    Console.WriteLine(_sum + " " + patternSum);

                    var calc = _sum * patternSum;
                    var last = int.Parse(calc.ToString().Last().ToString());

                    temp[j - 1] = last;
                }

                for (int j = 0; j < n; j++)
                {
                    List<int> pattern = patterns[j + 1];
                    int index = 1;
                    int sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        var val = input[k];
                        var times = pattern[index % pattern.Count];
                        var calc = val * times;
                        sum += calc;
                        index++;
                    }
                    Console.WriteLine(sum);
                    temp[j] = int.Parse(sum.ToString().Last().ToString());
                }
                return;

                input = temp;
            }

            string ans = string.Join("", input.Take(8));

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
            input = File.ReadAllLines(path).First().ToArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
