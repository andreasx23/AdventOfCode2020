using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day09
{
    public class Day9Part1
    {
        private string input = string.Empty;

        private void Day9()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StringBuilder output = new StringBuilder(), marker = new StringBuilder();
            bool isMarker = false;
            for (int i = 0; i < input.Length - 1; i++)
            {
                char c = input[i];

                if (c == '(' && char.IsDigit(input[i + 1]))
                {
                    isMarker = true;
                }
                else if (isMarker && c == ')')
                {
                    List<int> split = marker.ToString().Split('x').Select(int.Parse).ToList();
                    marker.Clear();
                    isMarker = false;

                    int amount = split[0];
                    int turns = split[1];

                    for (int j = 0; j < turns; j++)
                    {
                        string sub = input.Substring(i + 1, amount);
                        output.Append(sub);
                    }

                    i += amount;
                }
                else if (isMarker)
                {
                    marker.Append(c);
                }
                else
                {
                    output.Append(c);
                }
            }

            Console.WriteLine(output);

            int ans = output.Length;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day09\input.txt";
            input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
