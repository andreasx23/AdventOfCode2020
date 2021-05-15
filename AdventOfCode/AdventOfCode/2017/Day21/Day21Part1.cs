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
        private char[][] grid = new char[][]
        {
            new char[] { '.', '#', '.' },
            new char[] { '.', '.', '#' },
            new char[] { '#', '#', '#' },
        };
        private readonly List<(char[][] pattern, char[][] change)> rules = new List<(char[][] pattern, char[][] change)>();

        private void Day21()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 5; i++)
            {
                int gridWidth = grid[0].Length;
                int width = (gridWidth % 2 == 0) ? gridWidth / 2 : gridWidth / 3;
                List<char[][]> squares = (gridWidth % 2 == 0) ? GenerateSquares(grid, 2) : GenerateSquares(grid, 3);

                int n = squares.Count;
                for (int j = 0; j < n; j++)
                {
                    squares[j] = rules.First(arr => IsMatch(squares[j], arr.pattern)).change;
                }

                if (squares.Count == 1)
                {
                    grid = squares.First();
                    continue;
                }

                List<char[][]> newSquares = new List<char[][]>();
                for (int j = 0; j < n; j++)
                {
                    char[][] current = null;
                    if ( j % width == 0)
                    {
                        j++;
                        current = ConcatSquares(squares[j - 1], squares[j]);
                        newSquares.Add(current);
                    }
                    else
                    {
                        newSquares[newSquares.Count - 1] = ConcatSquares(newSquares.Last(), squares[j]);
                    }
                }

                char[][] newGrid = newSquares.First();
                for (int j = 1; j < newSquares.Count; j++)
                {
                    newGrid = newGrid.Concat(newSquares[j]).ToArray();
                }
                grid = newGrid;
            }

            int ans = grid.Select(array => array.Count(c => c == '#')).Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char[][] ConcatSquares(char[][] array1, char[][] array2)
        {
            List<char[]> output = new List<char[]>();

            int widthArray1 = array1[0].Length, widthArray2 = array2[0].Length, height = array1.Length;
            for (int i = 0; i < height; i++)
            {
                char[] temp = new char[widthArray1 + widthArray2];
                for (int j = 0; j < widthArray1; j++)
                {
                    temp[j] = array1[i][j];                    
                }

                for (int j = 0; j < widthArray2; j++)
                {
                    temp[widthArray1 + j] = array2[i][j];
                }
                output.Add(temp);
            }

            return output.ToArray();
        }

        private bool IsMatch(char[][] current, char[][] target)
        {
            int H = current.Length, W = current[0].Length;
            int TH = target.Length, TW = target[0].Length;

            if (H != TH && W != TW) return false;

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (current[i][j] != target[i][j]) return false;
                }
            }

            return true;
        }

        private List<char[][]> GenerateSquares(char[][] grid, int size)
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

        private char[][] FlipRight(char[][] grid)
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

        private char[][] FlipUpsideDown(char[][] grid)
        {
            int H = grid.Length;

            char[][] output = new char[H][];
            for (int i = 0; i < H; i++)
            {
                output[i] = grid[H - 1 - i];
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
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day21\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var (pattern, change) = RulePattern(s);
                rules.Add((pattern, change));

                for (int i = 0; i < 4; i++)
                {
                    pattern = FlipRight(pattern);
                    if (i + 1 < 4) rules.Add((pattern, change));
                }

                if (pattern.Length == 2) continue;

                pattern = FlipUpsideDown(pattern);
                rules.Add((pattern, change));

                for (int i = 0; i < 3; i++)
                {
                    pattern = FlipRight(pattern);
                    rules.Add((pattern, change));
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day21();
        }
    }
}
