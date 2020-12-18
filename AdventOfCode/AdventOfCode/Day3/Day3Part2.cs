using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Day3Part2
    {
        private List<string> input = new List<string>();

        private void Day3()
        {
            int column = input.Count, row = input[0].Length;

            long ans = 0;
            int[][] numbers = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { 3, 1 },
                new int[] { 5, 1 },
                new int[] { 7, 1 },
                new int[] { 1, 2 },
            };
            for (int loops = 0; loops < 5; loops++)
            {
                int right = numbers[loops][0], down = numbers[loops][1];
                int i = 0, j = 0, tempAns = 0;
                while (true)
                {
                    int moves = j + right;

                    if (moves <= row - 1)
                    {
                        j += right;
                    }
                    else if (moves > row - 1)
                    {
                        for (int k = 0; k < right; k++)
                        {
                            j++;

                            if (j == row)
                            {
                                j = 0;
                            }
                        }
                    }

                    i += down;

                    if (i >= column)
                    {
                        if (ans == 0)
                        {
                            ans += tempAns;
                        }
                        else
                        {
                            ans *= tempAns;
                        }

                        Console.WriteLine(tempAns);

                        break;
                    }

                    if (input[i][j] == '#')
                    {
                        tempAns++;
                    }
                }
            }

            Console.WriteLine(ans);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 3\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
