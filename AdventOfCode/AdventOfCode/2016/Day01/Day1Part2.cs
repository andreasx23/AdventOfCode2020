using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day01
{
    public class Day1Part2
    {
        private readonly List<(char direction, int amount)> input = new List<(char direction, int amount)>();

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int i = 0, j = 0;
            HashSet<(int x, int y)> isVisited = new HashSet<(int x, int y)>
            {
                (i, j)
            };

            int dir = 0;
            foreach (var (direction, amount) in input)
            {
                if (direction == 'R')
                {
                    dir++;
                }
                else
                {
                    dir--;
                }

                if (dir < 0)
                {
                    dir = 3;
                }
                else if (dir > 3)
                {
                    dir = 0;
                }

                bool isFinished = false;
                switch (dir)
                {
                    case 0:
                        {
                            for (int k = 0; k < amount; k++)
                            {
                                if (isVisited.Add((i - 1, j)))
                                {
                                    i--;                                    
                                }
                                else
                                {
                                    i--;
                                    isFinished = true;
                                    break;
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            for (int k = 0; k < amount; k++)
                            {
                                if (isVisited.Add((i, j + 1)))
                                {
                                    j++;                                    
                                }
                                else
                                {
                                    j++;
                                    isFinished = true;
                                    break;
                                }
                            }
                        }                        
                        break;
                    case 2:
                        {
                            for (int k = 0; k < amount; k++)
                            {
                                if (isVisited.Add((i + 1, j)))
                                {
                                    i++;                                    
                                }
                                else
                                {
                                    i++;
                                    isFinished = true;
                                    break;
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            for (int k = 0; k < amount; k++)
                            {
                                if (isVisited.Add((i, j - 1)))
                                {
                                    j--;
                                }
                                else
                                {
                                    j--;
                                    isFinished = true;
                                    break;                                    
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (isFinished)
                {
                    break;
                }
            }

            int ans = Math.Abs(i) + Math.Abs(j);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day01\input.txt";
            var lines = File.ReadAllLines(path)[0].Split(',').Select(s => s.Trim()).ToList();

            foreach (var instructions in lines)
            {
                char dir = instructions[0];
                int amount = Convert.ToInt32(instructions.Substring(1));
                input.Add((dir, amount));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
