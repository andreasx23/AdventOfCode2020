using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day05
{
    public class Day5Part2
    {
        private readonly StringBuilder input = new StringBuilder();

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            HashSet<(char first, char second)> setLookup = new HashSet<(char first, char second)>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                var upper = char.ToUpper(c);
                setLookup.Add((c, upper));
                setLookup.Add((upper, c));
            }

            int ans = int.MaxValue;
            for (char c = 'a'; c <= 'z'; c++)
            {
                StringBuilder copy = new StringBuilder();
                foreach (var inputChar in input.ToString())
                {
                    if (inputChar != c && inputChar != char.ToUpper(c))
                    {
                        copy.Append(inputChar);
                    }
                }

                bool isFinished = false;
                while (!isFinished)
                {
                    int change = 0;
                    for (int i = 1; i < copy.Length; i++)
                    {
                        char currentChar = copy[i], prevChar = copy[i - 1];
                        if (setLookup.Contains((currentChar, prevChar)) || setLookup.Contains((prevChar, currentChar)))
                        {
                            copy = copy.Remove(i - 1, 2);
                            change++;
                        }
                    }

                    if (change == 0)
                    {
                        isFinished = true;
                    }
                }
                ans = Math.Min(ans, copy.Length);
            }

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
