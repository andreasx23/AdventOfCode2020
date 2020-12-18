using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5
{
    public class Day5Part2
    {
        private List<string> input = new List<string>();

        /* https://adventofcode.com/2020/day/5#part2
         * 
         * 128 rows (0 - 127)
         * F = front (0 - 63)
         * B = back (64 - 127)
         * 
         * 8 columns (0 - 7)
         * L = left (0 - 3)
         * R = right (4 - 7)
         * 
         * F means to take the lower half, keeping rows 0 through 63.
         * B means to take the upper half, keeping rows 32 through 63.
         * F means to take the lower half, keeping rows 32 through 47.
         * B means to take the upper half, keeping rows 40 through 47.
         * B keeps rows 44 through 47.
         * F keeps rows 44 through 45.
         * The final F keeps the lower of the two, row 44.
         */
        private void Day5()
        {
            List<int> ids = new List<int>();
            foreach (var s in input)
            {
                int front = 0, back = 127, left = 0, right = 7, n = s.Length, row = 0, column = 0;
                for (int i = 0; i < n; i++)
                {
                    char c = s[i];
                    if (c == 'F') //Front
                    {
                        back = (front + back) / 2;
                    }
                    else if (c == 'B') //Back
                    {
                        front = (front + back) / 2 + 1;
                    }
                    else if (c == 'L') //Left
                    {
                        right = (left + right) / 2;
                    }
                    else if (c == 'R') //Right
                    {
                        left = (left + right) / 2 + 1;
                    }

                    if (i + 1 == 7 && front == back)
                    {
                        row = front;
                    }
                    else if (i + 1 == 10 && left == right)
                    {
                        column = left;
                    }
                }

                int sum = row * 8 + column;
                ids.Add(sum);
            }

            ids = ids.OrderBy(n => n).ToList();
            int index = ids[0];
            foreach (var num in ids)
            {
                if (index != num)
                {
                    Console.WriteLine("Answer: " + index);
                    break;
                }
                index++;
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 5\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
