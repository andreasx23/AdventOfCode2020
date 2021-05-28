using AlgoKit.Collections.Heaps;
using System;
using System.Collections.Concurrent;
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
            public List<Key> CollectedKeys = new List<Key>();
            public int X;
            public int Y;
            public int KeyCount;
            public int ItemsUsed;
        }

        class Key
        {
            public int X;
            public int Y;
            public char Value;

            public Key Clone()
            {
                return new Key()
                {
                    X = X,
                    Y = Y,
                    Value = Value
                };
            }
        }

        class Tile
        {
            public int X;
            public int Y;
            public int Cost;
            public int Distance;
            public int CostDistance => Cost + Distance;
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

            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, State>(comparer);

            List<Key> valids = new List<Key>();
            DFS(grid, x, y, new bool[H, W], new List<Key>(), valids);

            foreach (var key in valids)
            {
                var state = new State()
                {
                    Grid = CloneGrid(grid),
                    X = key.X,
                    Y = key.Y,
                    Steps = ShortestPath(x, y, key.X, key.Y, grid),
                    KeyCount = 1,
                    ItemsUsed = 1
                };
                state.CollectedKeys.Add(key);
                state.Grid[state.X][state.Y] = (char)Area.FLOOR;
                queue.Add(state.Steps, state);
            }

            int ans = int.MaxValue, runs = 0, maxKeys = 0;
            while (!queue.IsEmpty)
            {
                var current = queue.Pop().Value;

                if (current.KeyCount == keys.Count)
                {
                    if (current.Steps < ans)
                    {
                        ans = current.Steps;
                        Console.WriteLine(ans);
                    }
                    continue;
                }

                maxKeys = Math.Max(maxKeys, current.KeyCount);

                List<Key> localValids = new List<Key>();
                DFS(current.Grid, current.X, current.Y, new bool[H, W], current.CollectedKeys, localValids);

                Parallel.ForEach(localValids, key =>
                {
                    var newGrid = CloneGrid(current.Grid);
                    var steps = ShortestPath(current.X, current.Y, key.X, key.Y, current.Grid);
                    if (char.IsUpper(key.Value))
                    {
                        State state = new State()
                        {
                            X = key.X,
                            Y = key.Y,
                            KeyCount = current.KeyCount,
                            Grid = newGrid,
                            Steps = current.Steps + steps,
                            CollectedKeys = new List<Key>(current.CollectedKeys.Select(k => k.Clone())),
                            ItemsUsed = current.ItemsUsed + 1
                        };
                        state.Grid[key.X][key.Y] = '.';
                        state.CollectedKeys.Remove(key);
                        queue.Add(state.Steps, state);
                    }
                    else
                    {
                        State state = new State()
                        {
                            X = key.X,
                            Y = key.Y,
                            KeyCount = current.KeyCount + 1,
                            Grid = CloneGrid(current.Grid),
                            Steps = current.Steps + steps,
                            CollectedKeys = new List<Key>(current.CollectedKeys.Select(k => k.Clone())),
                            ItemsUsed = current.ItemsUsed + 1
                        };
                        state.Grid[key.X][key.Y] = '.';
                        state.CollectedKeys.Add(key);
                        queue.Add(state.Steps, state);
                    }
                });

                if (runs % 1000 == 0)
                {
                    Console.WriteLine($"Ran a total of {runs} times - Best amount of keys: {maxKeys}");
                }
                runs++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void DFS(char[][] grid, int x, int y, bool[,] visisted, List<Key> collectedKeys, List<Key> valids)
        {
            if (x < 0 || x >= grid.Length || y < 0 || y >= grid[x].Length || visisted[x, y] || grid[x][y] == (char)Area.WALL) return;

            if (char.IsUpper(grid[x][y]))
            {
                if (collectedKeys.Any(k => k.Value == char.ToLower(grid[x][y])))
                {
                    valids.Add(new Key()
                    {
                        Value = grid[x][y],
                        X = x,
                        Y = y
                    });
                }
                return;
            }
            else if (char.IsLower(grid[x][y]))
            {
                valids.Add(new Key()
                {
                    Value = grid[x][y],
                    X = x,
                    Y = y
                });
                return;
            }

            visisted[x, y] = true;

            DFS(grid, x + 1, y, visisted, collectedKeys, valids);
            DFS(grid, x - 1, y, visisted, collectedKeys, valids);
            DFS(grid, x, y + 1, visisted, collectedKeys, valids);
            DFS(grid, x, y - 1, visisted, collectedKeys, valids);
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private char[][] CloneGrid(char[][] grid)
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

        private int ShortestPath(int x, int y, int targetX, int targetY, char[][] grid)
        {
            Tile start = new Tile()
            {
                X = x,
                Y = y,
                Distance = CalculateManhattenDistance(x, y, targetX, targetY)
            };

            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, Tile>(comparer);
            bool[,] visisted = new bool[H, W];

            queue.Add(start.CostDistance, start);
            visisted[start.X, start.Y] = true;

            Dictionary<(int x, int y), int> map = new Dictionary<(int x, int y), int>
            {
                { (start.X, start.Y), start.CostDistance }
            };

            while (!queue.IsEmpty)
            {
                var current = queue.Pop().Value;

                if (current.X == targetX && current.Y == targetY)
                {
                    return current.Cost;
                }

                foreach (var next in ValidTiles(current, grid))
                {
                    next.Cost = current.Cost + 1;
                    next.Distance = CalculateManhattenDistance(next.X, next.Y, targetX, targetY);

                    if (map.TryGetValue((next.X, next.Y), out int CostDistance))
                    {
                        if (CostDistance > next.CostDistance)
                        {
                            map[(next.X, next.Y)] = next.CostDistance;
                            queue.Add(next.CostDistance, next);
                        }
                    }
                    else if (!visisted[next.X, next.Y])
                    {
                        visisted[next.X, next.Y] = true;
                        queue.Add(next.CostDistance, next);
                        map.Add((next.X, next.Y), next.CostDistance);
                    }
                }
            }

            return -1;
        }

        private List<Tile> ValidTiles(Tile current, char[][] grid)
        {
            List<Tile> dirs = new List<Tile>()
            {
                new Tile() { X = -1, Y = 0 },
                new Tile() { X = 1, Y = 0 },
                new Tile() { X = 0, Y = -1 },
                new Tile() { X = 0, Y = 1 },
            };

            List<Tile> valids = new List<Tile>();
            foreach (var next in dirs)
            {
                next.X += current.X;
                next.Y += current.Y;

                if (next.X >= 0 && next.X < H && next.Y >= 0 && next.Y < W && grid[next.X][next.Y] != (char)Area.WALL)
                {
                    valids.Add(next);
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
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2019\Day18\input.txt";
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
