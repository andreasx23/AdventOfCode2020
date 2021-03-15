using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day22
{
    public class Day22Part2
    {
        class Coordinate
        {
            public int X;
            public int Y;
            public char Type;
            public Tool Tool;
            public int Time;
            public Coordinate Parent;
        }

        enum Tool
        {
            ClimbingGear = 1,
            Torch = 2,
            Neither = 3
        }

        private int H = 0, W = 0, depth = 0, mod = 20183;
        private readonly Coordinate start = new Coordinate() { X = 0, Y = 0, Type = 'M', Tool = Tool.Torch, Time = 0 };
        private readonly Coordinate target = new Coordinate() { Type = 'T' };
        private char[][] grid = null;

        //https://adventofcode.com/2018/day/22
        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int[,] count = new int[H, W];
            //bool[,] isVisited = new bool[H, W];
            HashSet<(int x, int y, Tool tool)> isVisited = new HashSet<(int x, int y, Tool tool)>();
            int ans = int.MaxValue;

            Print(grid);
            Console.WriteLine();

            Queue<Coordinate> queue = new Queue<Coordinate>();
            queue.Enqueue(start);
            //isVisited[start.X, start.Y] = true;
            isVisited.Add((start.X, start.Y, start.Tool));

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.X == target.X && current.Y == target.Y)
                {
                    int time = current.Time + 7;
                    ans = Math.Min(ans, time);

                    Console.WriteLine("Current path:");
                    var clone = (char[][])grid.Clone();
                    var temp = current;
                    while (temp != null)
                    {
                        clone[temp.X][temp.Y] = 'X';
                        temp = temp.Parent;
                    }
                    clone[start.X][start.Y] = start.Type;
                    clone[target.X][target.Y] = target.Type;
                    Print(clone);
                    Console.WriteLine();
                    continue;
                }

                List<Coordinate> validCoordinates = ValidCoordinates(grid, current);
                foreach (var next in validCoordinates)
                {
                    next.Parent = current;

                    foreach (var tool in (Tool[])Enum.GetValues(typeof(Tool)))
                    {
                        next.Tool = tool;
                        if (isVisited.Contains((next.X, next.Y, next.Tool)))
                        {
                            continue;
                        }

                        isVisited.Add((next.X, next.Y, next.Tool));
                        if (CanPass(grid, next))
                        {
                            next.Time = current.Time + 1;
                        }
                        else
                        {
                            next.Time = current.Time + 7;
                        }

                        queue.Enqueue(new Coordinate() { X = next.X, Y = next.Y, Parent = next.Parent, Time = next.Time, Tool = next.Tool });
                    }
                }
            }

            //Print(grid);
            //Console.WriteLine();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Coordinate> ValidCoordinates(char[][] grid, Coordinate current)
        {
            List<Coordinate> directions = new List<Coordinate>()
            {
                new Coordinate() { X = -1, Y = 0 },
                new Coordinate() { X = 1, Y = 0 },
                new Coordinate() { X = 0, Y = -1 },
                new Coordinate() { X = 0, Y = 1 },
            };

            List<Coordinate> validCoordinates = new List<Coordinate>();
            foreach (var coordinate in directions)
            {
                coordinate.X += current.X;
                coordinate.Y += current.Y;

                if (coordinate.X < 0 || coordinate.X >= grid.Length || coordinate.Y < 0 || coordinate.Y >= grid[0].Length)
                {
                    continue;
                }

                validCoordinates.Add(coordinate);
            }

            return validCoordinates;
        }

        private List<Tool> ToolSelecter(char[][] grid, Coordinate current)
        {
            var type = grid[current.X][current.Y];
            if (type == '.')
            {
                return new List<Tool>() { Tool.Torch, Tool.ClimbingGear };
            }
            else if (type == '=')
            {
                return new List<Tool>() { Tool.ClimbingGear, Tool.Neither };
            }
            else
            {
                return new List<Tool>() { Tool.Torch, Tool.Neither };
            }
        }

        private bool CanPass(char[][] grid, Coordinate current)
        {
            if (current.Tool == Tool.Torch && grid[current.X][current.Y] == '.' || current.Tool == Tool.ClimbingGear && grid[current.X][current.Y] == '.') //Rocky
            {
                return true;
            }
            else if (current.Tool == Tool.ClimbingGear && grid[current.X][current.Y] == '=' || current.Tool == Tool.Neither && grid[current.X][current.Y] == '=') //Wet
            {
                return true;
            }
            else if (current.Tool == Tool.Torch && grid[current.X][current.Y] == '|' || current.Tool == Tool.Neither && grid[current.X][current.Y] == '|') //Narrow
            {
                return true;
            }

            return false;
        }

        private void Print(char[][] grid)
        {
            int index = 0;
            foreach (var item in grid)
            {
                Console.WriteLine(index++ + " " + string.Join("", item));
            }
        }

        private void ReadData()
        {
            const string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2018\Day22\sample.txt";
            var lines = File.ReadAllLines(path);
            var first = lines[0].Split(' ');
            var depth = int.Parse(first[1]);
            this.depth = depth;
            var second = lines[1].Split(' ')[1].Split(',');
            var X = int.Parse(second[1]);
            var Y = int.Parse(second[0]);
            target.X = X;
            target.Y = Y;

            if (depth > 510)
            {
                H = 800;
                W = 15;
            }
            else
            {
                H = 16;
                W = 16;
            }

            long[][] dp = new long[H][];
            grid = new char[H][];
            for (int x = 0; x < H; x++)
            {
                grid[x] = new char[W];
                dp[x] = new long[W];
            }

            dp[start.X][start.Y] = 0;
            dp[target.X][target.Y] = 0;

            for (int i = 1; i < H; i++)
            {
                long sum = i * 48271 + depth;
                long erosionLevel = sum % mod;
                dp[i][0] = erosionLevel;
            }

            for (int i = 1; i < W; i++)
            {
                long sum = i * 16807 + depth;
                long erosionLevel = sum % mod;
                dp[0][i] = erosionLevel;
            }

            for (int i = 1; i < H; i++)
            {
                for (int j = 1; j < W; j++)
                {
                    if (i == target.X && j == target.Y) continue;

                    long sum = dp[i - 1][j] * dp[i][j - 1] + depth;
                    long erosionLevel = sum % mod;
                    dp[i][j] = erosionLevel;
                }
            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    long result = dp[i][j] % 3;
                    if (result == 0)
                    {
                        grid[i][j] = '.'; //Rocky
                    }
                    else if (result == 1)
                    {
                        grid[i][j] = '='; //Wet
                    }
                    else if (result == 2)
                    {
                        grid[i][j] = '|'; //Narrow
                    }
                }
            }

            long temp = depth % mod;
            temp %= 3;
            long ans = temp * 2;
            for (int i = 0; i <= target.X; i++)
            {
                for (int j = 0; j <= target.Y; j++)
                {
                    if (grid[i][j] == '.')//Rocky
                    {
                        ans += 0;
                    }
                    else if (grid[i][j] == '=') //Wet
                    {
                        ans += 1;
                    }
                    else if (grid[i][j] == '|') //Narrow
                    {
                        ans += 2;
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}
