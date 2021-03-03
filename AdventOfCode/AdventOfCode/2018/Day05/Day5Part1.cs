using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day05
{
    public class Day5Part1
    {
        private StringBuilder input = new StringBuilder();

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            HashSet<(char first, char second)> set = new HashSet<(char first, char second)>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                var upper = char.ToUpper(c);
                set.Add((c, upper));
                set.Add((upper, c));
            }

            bool isFinished = false;
            while (!isFinished)
            {
                int change = 0;
                for (int i = 1; i < input.Length; i++)
                {
                    char currentChar = input[i], prevChar = input[i - 1];
                    if (set.Contains((currentChar, prevChar)) || set.Contains((prevChar, currentChar)))
                    {
                        input = input.Remove(i - 1, 2);
                        change++;
                    }
                }

                if (change == 0)
                {
                    isFinished = true;
                }
            }

            int ans = input.Length;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day05\input.txt";
            input.Append(File.ReadAllLines(path)[0]);
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
