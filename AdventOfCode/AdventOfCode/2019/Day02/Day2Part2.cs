using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day02
{
    public class Day2Part2
    {
        private List<int> input = new List<int>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int target = 19690720;
            int noun = -1, verb = -1;
            bool isFinished = false;
            for (int i = 0; i < 100 && !isFinished; i++)
            {
                for (int j = 0; j < 100 && !isFinished; j++)
                {
                    if (Calculate(i, j, new List<int>(input)) == target)
                    {
                        noun = i;
                        verb = j;
                        isFinished = true;
                    }
                }
            }
            int ans = 100 * noun + verb;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private int Calculate(int noun, int verb, List<int> list)
        {
            list[1] = noun;
            list[2] = verb;

            int n = list.Count;
            for (int i = 0; i < n; i += 4)
            {
                int opcode = list[i];

                if (opcode == 99) break;

                int _noun = list[i + 1];
                int _verb = list[i + 2];
                int first = list[_noun];
                int second = list[_verb];
                int targetIndex = list[i + 3];

                int sum = -1;
                if (opcode == 1) sum = first + second;
                else if (opcode == 2) sum = first * second;

                list[targetIndex] = sum;
            }

            return list[0];
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
