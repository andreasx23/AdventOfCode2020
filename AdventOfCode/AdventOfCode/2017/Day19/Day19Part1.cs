using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day19
{
    enum Lines
    {
        VERTICAL = '|',
        HORIZONTAL = '-',
        CROSS = '+'
    }

    enum Direction
    {
        Down = 1,
        Right = 2,
        Up = 3,
        Left = 4
    }

    public class Day19Part1
    {
        private char[][] grid;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int H = grid.Length, W = grid[0].Length;
            int i = 0, j = 0;
            for (int x = 0; x < W; x++)
            {
                if ((Lines)grid[0][x] == Lines.VERTICAL)
                {
                    j = x;
                    break;
                }
            }

            Direction dir = Direction.Down;
            string ans = string.Empty;
            int prevX = -1, prevY = -1;
            while (i != prevX && j != prevY)
            {
                prevX = i;
                prevY = j;

                switch (dir)
                {
                    case Direction.Down:
                        {
                            while (CanContinue(i, j))
                            {
                                if (char.IsLetter(grid[i][j])) ans += grid[i][j];
                                i++;
                            }

                            if (IsInRange(i, j + 1) && (Lines)grid[i][j + 1] == Lines.HORIZONTAL || IsInRange(i, j + 1) && char.IsLetter(grid[i][j + 1]))
                            {
                                dir = Direction.Right;
                                j++;
                            }
                            else if (IsInRange(i, j - 1) && (Lines)grid[i][j - 1] == Lines.HORIZONTAL || IsInRange(i, j - 1) && char.IsLetter(grid[i][j - 1]))
                            {
                                dir = Direction.Left;
                                j--;
                            }
                        }
                        break;
                    case Direction.Right:
                        {
                            while (CanContinue(i, j))
                            {
                                if (char.IsLetter(grid[i][j])) ans += grid[i][j];
                                j++;
                            }

                            if (IsInRange(i + 1, j) && (Lines)grid[i + 1][j] == Lines.VERTICAL || IsInRange(i + 1, j) && char.IsLetter(grid[i + 1][j]))
                            {
                                dir = Direction.Down;
                                i++;
                            }
                            else if (IsInRange(i - 1, j) && (Lines)grid[i - 1][j] == Lines.VERTICAL || IsInRange(i - 1, j) && char.IsLetter(grid[i - 1][j]))
                            {
                                dir = Direction.Up;
                                i--;
                            }
                        }
                        break;
                    case Direction.Up:
                        {
                            while (CanContinue(i, j))
                            {
                                if (char.IsLetter(grid[i][j])) ans += grid[i][j];
                                i--;
                            }

                            if (IsInRange(i, j + 1) && (Lines)grid[i][j + 1] == Lines.HORIZONTAL || IsInRange(i, j + 1) && char.IsLetter(grid[i][j + 1]))
                            {
                                dir = Direction.Right;
                                j++;
                            }
                            else if (IsInRange(i, j - 1) && (Lines)grid[i][j - 1] == Lines.HORIZONTAL || IsInRange(i, j - 1) && char.IsLetter(grid[i][j - 1]))
                            {
                                dir = Direction.Left;
                                j--;
                            }
                        }
                        break;
                    case Direction.Left:
                        {
                            while (CanContinue(i, j))
                            {
                                if (char.IsLetter(grid[i][j])) ans += grid[i][j];
                                j--;
                            }

                            if (IsInRange(i + 1, j) && (Lines)grid[i + 1][j] == Lines.VERTICAL || IsInRange(i + 1, j) && char.IsLetter(grid[i + 1][j]))
                            {
                                dir = Direction.Down;
                                i++;
                            }
                            else if (IsInRange(i - 1, j) && (Lines)grid[i - 1][j] == Lines.VERTICAL || IsInRange(i - 1, j) && char.IsLetter(grid[i - 1][j]))
                            {
                                dir = Direction.Up;
                                i--;
                            }
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsInRange(int x, int y)
        {
            return x >= 0 && x < grid.Length && y >= 0 && y < grid[0].Length;
        }

        private bool CanContinue(int x, int y)
        {
            return IsInRange(x, y) && (Lines)grid[x][y] != Lines.CROSS && grid[x][y] != ' ';
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day19\input.txt";
            var lines = File.ReadAllLines(path);

            int n = lines.Length;
            grid = new char[n][];
            for (int i = 0; i < n; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
