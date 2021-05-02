using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day01
{
    public class Day1Part2
    {
        private string input = string.Empty;

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0, depth = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '(') 
                {
                    depth++;
                }
                else
                {
                    depth--;
                }

                if (depth == -1)
                {
                    ans = i + 1;
                    break;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day01\input.txt";
            input = File.ReadAllLines(path).First();
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
