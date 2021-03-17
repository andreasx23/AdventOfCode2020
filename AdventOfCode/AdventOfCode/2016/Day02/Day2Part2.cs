using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day02
{
    public class Day2Part2
    {
        private List<string> input = new List<string>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            char[][] grid = new char[5][];
            for (int _i = 0; _i < grid.Length; _i++)
            {
                grid[_i] = new char[5];
                for (int _j = 0; _j < grid[_i].Length; _j++)
                {
                    grid[_i][_j] = ' ';
                }
            }
            
            for (int _i = 1; _i < grid.Length - 1; _i++)
            {
                int sum = _i + 1;
                grid[1][_i] = sum.ToString()[0];
                grid[3][_i] = Convert.ToChar(_i - 1 + 'A');
            }

            for (int _i = 0; _i < grid.Length; _i++)
            {
                int sum = _i + 5;
                grid[2][_i] = sum.ToString()[0];
            }

            grid[0][2] = '1';
            grid[4][2] = 'D';

            string ans = "";
            int i = 2, j = 0;
            foreach (var s in input)
            {
                foreach (var c in s)
                {
                    switch (c)
                    {
                        case 'U':
                            {
                                if (i > 0 && grid[i - 1][j] != ' ')
                                {
                                    i--;
                                }
                            }
                            break;
                        case 'D':
                            {
                                if (i < 4 && grid[i + 1][j] != ' ')
                                {
                                    i++;
                                }
                            }
                            break;
                        case 'L':
                            {
                                if (j > 0 && grid[i][j - 1] != ' ')
                                {
                                    j--;
                                }
                            }
                            break;
                        case 'R':
                            {
                                if (j < 4 && grid[i][j + 1] != ' ')
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
