using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day02
{
    public class Day2Part1
    {
        private List<string> input = new List<string>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int[][] grid = new int[3][];
            int count = 1;
            for (int _i = 0; _i < 3; _i++)
            {
                grid[_i] = new int[3];
                for (int _j = 0; _j < grid[_i].Length; _j++)
                {
                    grid[_i][_j] = count++;
                }
            }

            string ans = "";
            int i = 1, j = 1;
            foreach (var s in input)
            {
                foreach (var c in s)
                {
                    switch (c)
                    {
                        case 'U':
                            {
                                if (i > 0)
                                {
                                    i--;
                                }
                            }
                            break;
                        case 'D':
                            {
                                if (i < 2)
                                {
                                    i++;
                                }
                            }
                            break;
                        case 'L':
                            {
                                if (j > 0)
                                {
                                    j--;
                                }
                            }
                            break;
                        case 'R':
                            {
                                if (j < 2)
                                {
                                    j++;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ans += grid[i][j].ToString();
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day02\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
