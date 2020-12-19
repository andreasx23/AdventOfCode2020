using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Day3Part1
    {
        private List<string> input = new List<string>();

        private void Day3()
        {
            int column = input.Count, row = input[0].Length;

            int right = 3, down = 1;
            int i = 0, j = 0, ans = 0;
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
                    break;
                }

                if (input[i][j] == '#')
                {
                    ans++;
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
