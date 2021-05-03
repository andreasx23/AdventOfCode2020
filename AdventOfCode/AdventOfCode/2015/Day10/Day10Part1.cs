using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day10
{
    public class Day10Part1
    {
        private string input = "1321131112";

        private void Day10()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 40; i++)
            {
                StringBuilder sb = new StringBuilder();

                int j = 0, n = input.Length;
                while (j < n)
                {
                    var current = input[j];
                    int len = 1;
                    while (j + 1 != n && current == input[j + 1])
                    {
                        j++;
                        len++;
                    }
                    sb.Append(len.ToString() + "" + current);
                    j++;
                }

                input = sb.ToString();
            }

            int ans = input.Length;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day01\input.txt";
            //input = File.ReadAllLines(path).First();
        }

        public void TestCase()
        {
            ReadData();
            Day10();
        }
    }
}
