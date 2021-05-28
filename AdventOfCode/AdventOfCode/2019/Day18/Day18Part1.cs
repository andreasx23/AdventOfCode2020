using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day18
{
    public class Day18Part1
    {
        private char[][] grid;
        private int H, W;
        private readonly List<(char key, int x, int y)> keys = new List<(char key, int x, int y)>();
        private readonly List<char> targets = new List<char>();

        enum Area
        {
            ENTRANCE = '@',
            FLOOR = '.',
            WALL = '#'
        }

        class State
        {
            public int Steps;
            public char[][] Grid;
            public List<char> CollectedKeys = new List<char>();
            public HashSet<char> targets = new HashSet<char>();
            public int X;
            public int Y;
        }

        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int x = 0, y = 0;
            bool isFound = false;
            for (int i = 0; i < H && !isFound; i++)
            {
                for (int j = 0; j < W && !isFound; j++)
                {
                    if (grid[i][j] == (char)Area.ENTRANCE)
                    {
                        x = i;
                        y = j;
                    }
                }
            }

            Queue<State> queue = new Queue<State>();
            foreach (var item in keys)
            {
                var output = CanReach(x, y, item.key, grid);
                if (output.isReable)
                {
                    var state = new State()
                    {
                        Grid = Copy(grid),
                        X = output.x,
                        Y = output.y,
                        Steps = output.steps
                    };
                    state.targets = new HashSet<char>(targets);
                    state.targets.Remove(item.key);
                    state.CollectedKeys.Add(item.key);
                    state.Grid[state.X][state.Y] = (char)Area.FLOOR;
                    queue.Enqueue(state);
                }
            }

            int ans = 0, runs = 0;
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.targets.Count == 0)
                {
                    ans = current.Steps;
                    break;
                }

                foreach (var target in targets)
                {
                    if (current.targets.Contains(target))
                    {
                        if (char.IsLower(target))
                        {
                            var output = CanReach(current.X, current.Y, target, current.Grid);
                            if (output.isReable)
                            {
                                var state = new State()
                                {
                                    Grid = Copy(current.Grid),
                                    X = output.x,
                                    Y = output.y
                                };
                                state.Steps = current.Steps + output.steps;
                                state.targets = new HashSet<char>(current.targets);
                                state.targets.Remove(target);
                                state.CollectedKeys.Add(target);
                                state.Grid[state.X][state.Y] = (char)Area.FLOOR;
                                queue.Enqueue(state);
                            }
                        }
                        else if (char.IsUpper(target) && current.CollectedKeys.Contains(char.ToLower(target)))
                        {
                            var output = CanReach(current.X, current.Y, target, current.Grid);
                            if (output.isReable)
                            {
                                var state = new State()
                                {
                                    Grid = Copy(current.Grid),
                                    X = output.x,
                                    Y = output.y
                                };
                                state.Steps = current.Steps + output.steps;
                                state.targets = new HashSet<char>(current.targets);
                                state.targets.Remove(target);
                                state.CollectedKeys.Remove(char.ToLower(target));
                                state.Grid[state.X][state.Y] = (char)Area.FLOOR;
                                queue.Enqueue(state);
                            }
                        }
                    }
                }

                if (runs % 1000 == 0) Console.WriteLine($"Ran a total of {runs} times");
                runs++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private char[][] Copy(char[][] grid)
        {
            char[][] result = new char[H][];

            for (int i = 0; i < H; i++)
            {
                result[i] = new char[W];
                for (int j = 0; j < W; j++)
                {
                    result[i][j] = grid[i][j];
                }
            }

            return result;
        }

        private (bool isReable, int x, int y, int steps) CanReach(int x, int y, char target, char[][] grid)
        {
            Queue<(int x, int y, int steps)> queue = new Queue<(int x, int y, int steps)>();
            HashSet<(int x, int y)> visisted = new HashSet<(int x, int y)>();
            queue.Enqueue((x, y, 0));
            visisted.Add((x, y));
            
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (grid[current.x + 1][current.y] == target)
                {
                    return (true, current.x + 1, current.y, current.steps + 1);
                }
                else if (grid[current.x - 1][current.y] == target)
                {
                    return (true, current.x - 1, current.y, current.steps + 1);
                }
                else if (grid[current.x][current.y + 1] == target)
                {
                    return (true, current.x, current.y + 1, current.steps + 1);
                }
                else if (grid[current.x][current.y - 1] == target)
                {
                    return (true, current.x, current.y - 1, current.steps + 1);
                }

                foreach (var item in ValidTiles(current.x, current.y, grid))
                {
                    if (visisted.Add((item.x, item.y)))
                    {
                        queue.Enqueue((item.x, item.y, current.steps + 1));
                    }
                }
            }

            return (false, -1, -1, -1);
        }

        private List<(int x, int y)> ValidTiles(int x, int y, char[][] grid)
        {
            List<(int x, int y)> dirs = new List<(int x, int y)>()
            {
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1)
            };

            List<(int x, int y)> valids = new List<(int x, int y)>();
            foreach (var item in dirs)
            {
                var dx = item.x + x;
                var dy = item.y + y;

                if (dx >= 0 && dx < H && dy >= 0 && dy < W && grid[dx][dy] == (char)Area.FLOOR)
                {
                    valids.Add((dx, dy));
                } 
            }

            return valids;
        }

        private int CalculateManhattenDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day18\input.txt";
            var lines = File.ReadAllLines(path);
            H = lines.Length;
            W = lines.First().Length;
            grid = new char[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    char c = grid[i][j];
                    if (char.IsLower(c))
                    {
                        keys.Add((c, i, j));
                        targets.Add(c);
                    }
                    else if (char.IsUpper(c))
                    {
                        targets.Add(c);
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
