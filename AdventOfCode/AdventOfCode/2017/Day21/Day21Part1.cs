using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day21
{
    public class Day21Part1
    {
        private char[][] grid = new char[3][];
        private readonly List<(char[][] pattern, char[][] change)> rules = new List<(char[][] pattern, char[][] change)>();

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int H = 3, W = 3;
            for (int i = 0; i < H; i++)
            {
                grid[i] = new char[W];
                for (int j = 0; j < W; j++)
                {
                    if (i + 1 == H)
                    {
                        grid[i][j] = '#';
                    }
                    else
                    {
                        grid[i][j] = '.';
                    }
                }
            }
            grid[0][1] = '#';
            grid[1][2] = '#';

            List<char[][]> grids = new List<char[][]> { grid };
            for (int i = 0; i < 5; i++)
            {
                List<char[][]> temp = new List<char[][]>();
                foreach (var grid in grids)
                {
                    foreach (var output in (grid.Length % 2 == 0) ? GenerateGrids(grid, 2) : GenerateGrids(grid, 3))
                    {
                        temp.Add(output);
                    }
                }

                for (int j = 0; j < temp.Count; j++)
                {
                    char[][] current = temp[j];
                    char[][] change = rules.First(arr => IsMatch(current, arr.pattern)).change;
                    Console.WriteLine("Count: " + rules.Count(arr => IsMatch(current, arr.pattern)));
                    temp[j] = change;
                }

                grids = new List<char[][]>(temp);
            }

            //Console.WriteLine(grids.Count);
            //Console.WriteLine("Higher than: 136");

            int ans = grids.Select(grid => grid.Select(array => array.Count(c => c == '#')).Sum()).Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsMatch(char[][] current, char[][] target)
        {
            int H = current.Length, W = current[0].Length;

            if (H != target.Length && W != target[0].Length) return false;

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (current[i][j] != target[i][j]) return false;
                }
            }
            return true;
        }

        private List<char[][]> GenerateGrids(char[][] grid, int size)
        {
            int H = grid.Length, W = grid[0].Length;
            List<char[][]> grids = new List<char[][]>();
            for (int j = 0; j < H; j += size)
            {
                for (int k = 0; k < W; k += size)
                {
                    char[][] temp = new char[size][];
                    for (int m = 0; m < size; m++)
                    {
                        temp[m] = new char[size];
                        for (int x = 0; x < size; x++)
                        {
                            temp[m][x] = grid[j + m][k + x];
                        }
                    }
                    grids.Add(temp);
                }
            }
            return grids;
        }

        private (char[][] pattern, char[][] change) RulePattern(string input)
        {
            var split = input.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
            var pattern = split.First().Split('/').Select(s => s.ToCharArray()).ToArray();
            var change = split.Last().Split('/').Select(s => s.ToCharArray()).ToArray();
            return (pattern, change);
        }

        private char[][] RotateRight(char[][] grid)
        {
            int H = grid.Length, W = grid[0].Length;
            char[][] output = new char[H][];
            for (int i = 0; i < H; i++)
            {
                output[i] = new char[W];
            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    output[j][W - 1 - i] = grid[i][j];
                }
            }

            return output;
        }

        private char[][] Flip(char[][] grid)
        {
            int H = grid.Length;
            char[][] output = new char[H][];

            for (int i = 0; i < H; i++)
            {
                output[H - 1 - i] = grid[i];
            }

            return output;
        }

        private void Print(char[][] grid)
        {
            foreach (var a in grid)
            {
                Console.WriteLine(string.Join("", a));
            }
            Console.WriteLine();
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2017\Day21\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var (pattern, change) = RulePattern(s);
                rules.Add((pattern, change));

                for (int i = 0; i < 4; i++)
                {
                    pattern = RotateRight(pattern);

                    if (i + 1 < 4) rules.Add((pattern, change));
                }

                if (pattern.Length == 2) continue;

                pattern = Flip(pattern);
                rules.Add((pattern, change));

                for (int i = 0; i < 3; i++)
                {
                    pattern = RotateRight(pattern);
                    rules.Add((pattern, change));
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
