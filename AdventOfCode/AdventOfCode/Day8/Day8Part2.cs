using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
{
    public class Day8Part2
    {
        private List<string> input = new List<string>();

        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int n = input.Count, index = 0, accumulator = 0;            
            int prevIndex = -1, prevAccumulator = -1, turnsBeforeReset = 5000;
            HashSet<int> isVisted = new HashSet<int>();
            bool isChanged = false;
            while (index + 1 != n)
            {
                var split = input[index].Split(' ');
                string instruction = split[0];
                int amount = int.Parse(split[1]);

                if (!isChanged && !instruction.Equals("acc") && !isVisted.Contains(index))
                {
                    isVisted.Add(index);
                    isChanged = true;
                    prevIndex = index;                    
                    prevAccumulator = accumulator;
                    instruction = (instruction.Equals("nop")) ? "jmp" : "nop";
                }
                
                int temp = index;
                bool isNop = false;
                if (instruction.Equals("nop"))
                {
                    temp += amount;
                    isNop = true;
                }
                else if (instruction.Equals("acc"))
                {
                    accumulator += amount;
                    temp++;
                }
                else //jmp
                {
                    temp += amount;
                }

                int tempN = n - 1;
                if (temp >= 0 && temp <= tempN)
                {
                    index = (!isNop) ? temp : index + 1;
                }
                else if (temp > tempN)
                {
                    int runs = temp - index;
                    for (int run = 0; run < runs; run++)
                    {
                        temp++;

                        if (temp == n)
                        {
                            temp = 0;
                        }
                    }
                    index = temp;
                }

                turnsBeforeReset--;
                if (turnsBeforeReset == 0)
                {
                    index = prevIndex;
                    isChanged = false;
                    turnsBeforeReset = 5000;
                    accumulator = prevAccumulator;
                }
            }
            watch.Stop();

            Console.WriteLine("Answer: " + accumulator + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 8\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
