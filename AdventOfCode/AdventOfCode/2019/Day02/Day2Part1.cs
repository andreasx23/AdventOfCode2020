using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day02
{
    public class Day2Part1
    {
        private List<int> input = new List<int>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            input[1] = 12;
            input[2] = 2;

            int n = input.Count;
            for (int i = 0; i < n; i += 4)
            {
                int opcode = input[i];

                if (opcode == 99) break;

                int noun = input[i + 1];
                int verb = input[i + 2];
                int first = input[noun];
                int second = input[verb];
                int targetIndex = input[i + 3];

                int sum = -1;
                if (opcode == 1)
                {
                    sum = first + second;
                }
                else if (opcode == 2)
                {
                    sum = first * second;
                }

                input[targetIndex] = sum;
            }
            int ans = input[0];

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day02\input.txt";
            input = File.ReadAllLines(path)[0].Split(',').Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
