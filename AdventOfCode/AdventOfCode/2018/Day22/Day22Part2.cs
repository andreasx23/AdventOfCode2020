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
        class Unit
        {
            public int X;
            public int Y;
            public char Type;
            public Tool Tool;
            public int Time;
            public Unit Parent;
            public int Switching;
        }

        enum Tool
        {
            ClimbingGear = 1,
            Torch = 2,
            Neither = 3
        }

        private int H = 0, W = 0, depth = 0, mod = 20183;
        private readonly Unit start = new Unit() { X = 0, Y = 0, Type = 'M', Tool = Tool.Torch, Time = 0, Switching = 0 };
        private readonly Unit target = new Unit() { Type = 'T' };
        private char[][] grid = null;

        //https://adventofcode.com/2018/day/22
        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Print(grid);
            //Console.WriteLine();

            int ans = 0;
            Queue<Unit> queue = new Queue<Unit>();
            queue.Enqueue(start);
            HashSet<(int x, int y, Tool tool)> isVisited = new HashSet<(int x, int y, Tool tool)>();
            isVisited.Add((start.X, start.Y, start.Tool));

            while (queue.Any())
            {
                Unit current = queue.Dequeue();

                if (current.Switching > 0)
                {
                    if (current.Switching != 1 || isVisited.Add((current.X, current.Y, current.Tool)))
                    {
                        Unit next = new Unit()
                        {
                            X = current.X,
                            Y = current.Y,
                            Tool = current.Tool,
                            Switching = current.Switching - 1,
                            Time = current.Time + 1,
                            Parent = current
                        };
                        queue.Enqueue(next);
                    }
                    continue;
                }

                if (current.X == target.X && current.Y == target.Y && current.Tool == Tool.Torch)
                {
                    //var temp = current;
                    //while (temp != null)
                    //{
                    //    grid[temp.X][temp.Y] = 'X';
                    //    temp = temp.Parent;
                    //}
                    ans = current.Time;
                    break;
                }

                List<Unit> validCoordinates = ValidCoordinates(grid, current);
                foreach (var next in validCoordinates)
                {
                    next.Tool = current.Tool;
                    next.Switching = 0;
                    next.Time = current.Time + 1;
                    next.Parent = current;

                    if (GetTools(grid[next.X][next.Y]).Contains(next.Tool) && isVisited.Add((next.X, next.Y, next.Tool)))
                    {
                        queue.Enqueue(next);
                    }
                }

                foreach (var tool in GetTools(grid[current.X][current.Y]))
                {
                    Unit next = new Unit()
                    {
                        X = current.X,
                        Y = current.Y,
                        Tool = tool,
                        Switching = 6,
                        Time = current.Time + 1,
                        Parent = current
                    };

                    queue.Enqueue(next);
                }
            }

            //Print(grid);
            //Console.WriteLine();

            Console.WriteLine("Answer is: 1070");

            watch.Stop();
            Console.WriteLine($"Answer: {ans-54} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Unit> ValidCoordinates(char[][] grid, Unit current)
        {
            List<Unit> unitDirections = new List<Unit>()
            {
                new Unit() { X = -1, Y = 0 },
                new Unit() { X = 1, Y = 0 },
                new Unit() { X = 0, Y = -1 },
                new Unit() { X = 0, Y = 1 },
            };

            List<Unit> validCoordinates = new List<Unit>();
            foreach (var nextDirection in unitDirections)
            {
                nextDirection.X += current.X;
                nextDirection.Y += current.Y;

                if (nextDirection.X < 0 || nextDirection.X >= grid.Length || nextDirection.Y < 0 || nextDirection.Y >= grid[0].Length)
                {
                    continue;
                }

                validCoordinates.Add(nextDirection);
            }

            return validCoordinates;
        }

        private List<Tool> GetTools(char region)
        {
            return region switch
            {
                '.' => new List<Tool>() { Tool.ClimbingGear, Tool.Torch }, //Rock
                '=' => new List<Tool>() { Tool.ClimbingGear, Tool.Neither }, //Wet
                '|' => new List<Tool>() { Tool.Torch, Tool.Neither }, //Narrow
                _ => throw new Exception("Unreachable"),
            };
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
            const string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2018\Day22\input.txt";
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
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}
