using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
{
    public class Day8Part1
    {
        private List<string> input = new List<string>();

        private void Day8()
        {
            int acc = 0;

            int n = input.Count;
            bool[] visited = new bool[n];
            int i = 0;
            while (!visited[i])
            {
                visited[i] = true;
                var split = input[i].Split(' ');
                string instruction = split[0];
                int amount = int.Parse(split[1]);

                int temp = i;
                bool isNop = false;
                if (instruction.Equals("nop")) 
                {
                    temp += amount;
                    isNop = true;
                }
                else if (instruction.Equals("acc"))
                {
                    acc += amount;
                    temp++;
                }
                else //jmp
                {
                    temp += amount;
                }

                int tempN = n - 1;
                if (temp >= 0 && temp <= tempN)
                {
                    i = (!isNop) ? temp : i + 1;
                }
                else if (temp > tempN)
                {
                    int runs = temp - i;
                    for (int run = 0; run < runs; run++)
                    {
                        temp++;

                        if (temp == n)
                        {
                            temp = 0;
                        }
                    }
                    i = temp;
                }
            }

            Console.WriteLine("Answer: " + acc);
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
