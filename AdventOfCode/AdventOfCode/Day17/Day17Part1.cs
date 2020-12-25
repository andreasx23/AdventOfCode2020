using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17
{
    public class Day17Part1
    {
        private char[][][] grid = new char[3][][];

        /* I don't have the slightest idea how to solve this problem
         * 
         * # = active
         * . = inative
         */
        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Console.WriteLine("After 0 cycles");
            Print(grid);

            for (int cycle = 0; cycle < 6; cycle++)
            {
                for (int i = 0; i < grid.Length; i++)
                {
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        for (int k = 0; k < grid[i][j].Length; k++)
                        {
                            var neighbours = GetNeighbours(i, j, k);
                            var count = neighbours.Count(c => c == '#');

                            char c = grid[i][j][k];
                            if (c == '#')
                            {
                                if (count != 2 || count != 3)
                                {
                                    grid[i][j][k] = '.';
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    grid[i][j][k] = '#';
                                }
                            }
                        }
                    }
                    Console.WriteLine($"After {i + 1} cycles");
                    Print(grid);
                }
            }

            int ans = GetCubes();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private int GetCubes()
        {
            int ans = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    ans += grid[i][j].Count(c => c == '#');
                }
            }
            return ans;
        }

        private IEnumerable<char> GetNeighbours(int _i, int _j, int _k)
        {
            int columns = grid[0].Length, rows = grid[0][0].Length;
            for (int i = _j - 1; i <= _j + 1; ++i)
            {
                for (int j = _k - 1; j <= _k + 1; ++j)
                {
                    if (i >= 0 && i < rows && j >= 0 && j < columns && !(i == _j && j == _k))
                    {
                        yield return grid[_i][i][j];
                    }
                }
            }
        }

        private void Print(char[][][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                Console.WriteLine("Z: " + (i - 1));
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.WriteLine(string.Join(" ", grid[i][j]));
                }
                Console.WriteLine();
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 17\sample.txt";
            var input = File.ReadAllLines(path);

            int n = input.Length;
            char[][] matrix = new char[n][];
            for (int i = 0; i < n; i++)
            {
                var sArr = input[i].ToCharArray();
                matrix[i] = sArr;
            }

            grid[0] = matrix;
            for (int i = 1; i < grid.GetLength(0); i++)
            {
                var h = grid[0].Length;
                char[][] height = new char[h][];
                for (int j = 0; j < h; j++)
                {
                    var w = grid[0][j].Length;
                    char[] arr = new char[w];
                    for (int k = 0; k < arr.Length; k++)
                    {
                        arr[k] = '.';
                    }

                    height[j] = arr;
                }

                grid[i] = height;
            }
        }

        public void TestCase()
        {
            ReadData();
            Day17();
        }
    }
}
