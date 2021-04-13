using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day18
{
    public class Day18Part1
    {
        private char[][] grid;

        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int H = grid.Length, W = grid[0].Length;
            for (int i = 0; i < 100; i++)
            {
                char[][] copy = Copy(grid);
                for (int j = 0; j < H; j++)
                {
                    for (int k = 0; k < W; k++)
                    {
                        char val = grid[j][k];
                        int count = Count(j, k);
                        if (val == '#')
                        {
                            if (count == 2 || count == 3)
                            {
                                continue;
                            }
                            else
                            {
                                copy[j][k] = '.';
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                copy[j][k] = '#';
                            }
                        }
                    }
                }
                grid = copy;
            }

            int ans = 0;
            foreach (var array in grid)
            {
                ans += array.Count(c => c == '#');
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char[][] Copy(char[][] input)
        {
            int H = input.Length, W = input[0].Length;
            char[][] grid = new char[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = new char[W];
                for (int j = 0; j < W; j++)
                {
                    grid[i][j] = input[i][j];
                }
            }
            return grid;
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private int Count(int x, int y)
        {
            List<(int x, int y)> coords = new List<(int x, int y)>()
            {
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1),
                (-1, -1),
                (1, -1),
                (-1, 1),
                (1, 1)
            }; 

            int count = 0;
            foreach (var next in coords)
            {
                int dx = next.x + x;
                int dy = next.y + y;

                if (dx < 0 || dx >= grid.Length || dy < 0 || dy >= grid[0].Length)
                {
                    continue;
                }

                if (grid[dx][dy] == '#')
                {
                    count++;
                }
            }

            return count;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day18\input.txt";
            var input = File.ReadAllLines(path);

            int n = input.Length;
            grid = new char[n][];
            for (int i = 0; i < n; i++)
            {
                grid[i] = input[i].ToCharArray();
            }
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
