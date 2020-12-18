using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    public class Day9Part1
    {
        private List<long> input = new List<long>();

        private void Day9()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            List<long> currentPreamble = new List<long>();
            int preambleTurns = 25;
            for (int i = 0; i < preambleTurns; i++)
            {
                currentPreamble.Add(input[i]);
            }

            int n = input.Count;
            long ans = 0;
            for (int i = preambleTurns; i < n; i++)
            {
                long num = input[i];
                bool isMatch = false;

                for (int j = 0; j < preambleTurns; j++)
                {
                    for (int k = j + 1; k < preambleTurns; k++)
                    {
                        long sum = currentPreamble[j] + currentPreamble[k];

                        if (sum == num)
                        {
                            
                            isMatch = true;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        break;
                    }
                }

                if (isMatch)
                {
                    currentPreamble.Add(num);
                    currentPreamble.RemoveAt(0);
                }
                else
                {
                    ans = num;
                    break;
                }
            }
            
            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 9\input.txt";
            input = File.ReadAllLines(path).Select(s => long.Parse(s)).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
