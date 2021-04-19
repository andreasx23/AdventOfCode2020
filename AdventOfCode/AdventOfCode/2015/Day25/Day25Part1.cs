using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day25
{
    public class Day25Part1
    {
        private readonly int xPos = 3010;
        private readonly int yPos = 3019;

        /*
         * ABCD
         * EFGH
         * IJKL
         * MNOP
         * 
         * (0, 0)
         * (1, 0), (0, 1)
         * (2, 0), (1, 1), (0, 2)
         * (3, 0), (2, 1), (1, 2), (0, 3)
         * (3, 1), (2, 2), (1, 3)
         * (3, 2), (2, 3)
         * (3, 3)
         */
        private void Day25()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int H = 10000, W = 10000;
            long[][] grid = new long[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = new long[W];
            }

            long prev = 20151125;
            grid[0][0] = prev;
            grid[H - 1][W - 1] = long.MaxValue;
            int x = 1, y = 0;

            bool isLowestRow = false;
            while (grid[H - 1][W - 1] == long.MaxValue)
            {
                int tempX = x, tempY = y;
                while (tempX >= 0 && tempY < W)
                {
                    grid[tempX][tempY] = GenerateNextNumber(prev);
                    prev = grid[tempX][tempY];
                    tempX--;
                    tempY++;
                }

                if (isLowestRow)
                {
                    if (y + 1 != W)
                    {
                        y++;
                    }
                }
                else
                {
                    x++;

                    if (x == H)
                    {
                        isLowestRow = true;
                        x--;
                        y++;
                    }
                }
            }

            long ans = grid[xPos - 1][yPos - 1];

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private long GenerateNextNumber(long prev)
        {
            return (prev * 252533) % 33554393;
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day22\input.txt";
            //var lines = File.ReadAllLines(path);
        }

        public void TestCase()
        {
            ReadData();
            Day25();
        }
    }
}
