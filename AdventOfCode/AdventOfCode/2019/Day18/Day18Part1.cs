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
        private char[][] _grid;
        private int _H, _W;
        private int _keyCount = 0;
        private readonly List<Target> _targets = new List<Target>();

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
            public List<Target> CollectedKeys = new List<Target>();
            public Target Target;
            public int KeyCount;
        }

        class Target
        {
            public int X;
            public int Y;
            public char Value;

            public Target Clone()
            {
                return new Target()
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
            for (int i = 0; i < _H && !isFound; i++)
            {
                for (int j = 0; j < _W && !isFound; j++)
                {
                    if (_grid[i][j] == (char)Area.ENTRANCE)
                    {
                        x = i;
                        y = j;
                        isFound = true;
                    }
                }
            }

            Dictionary<char, Dictionary<char, int>> distances = new Dictionary<char, Dictionary<char, int>>()
            {
                { (char)Area.ENTRANCE, new Dictionary<char, int>() }
            };

            foreach (var target in _targets)
            {
                var distance = CalculateShortestPath(x, y, target.X, target.Y, _grid);
                distances[(char)Area.ENTRANCE].Add(target.Value, distance);
            }

            foreach (var key in _targets)
            {
                distances.Add(key.Value, new Dictionary<char, int>());
                foreach (var target in _targets)
                {
                    if (key.Value == target.Value) continue;
                    var distance = CalculateShortestPath(key.X, key.Y, target.X, target.Y, _grid);
                    distances[key.Value].Add(target.Value, distance);
                }
            }

            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, State>(comparer);

            List<Target> reachableTargets = new List<Target>();
            CalculateReachableTargets(_grid, x, y, new bool[_H, _W], new List<Target>(), reachableTargets);

            foreach (var key in reachableTargets)
            {
                var state = new State()
                {
                    Grid = CloneGrid(_grid),
                    Steps = distances[(char)Area.ENTRANCE][key.Value],
                    KeyCount = 1,
                    Target = key
                };
                state.CollectedKeys.Add(key);
                state.Grid[state.Target.X][state.Target.Y] = (char)Area.FLOOR;
                queue.Add(-state.KeyCount, state);
            }

            int ans = int.MaxValue, runs = 0, completedPaths = 0;
            while (!queue.IsEmpty)
            {
                var current = queue.Pop().Value;

                if (current.KeyCount == _keyCount)
                {
                    ans = Math.Min(ans, current.Steps);
                    completedPaths++;
                    continue;
                }

                List<Target> _reachableTargets = new List<Target>();
                CalculateReachableTargets(current.Grid, current.Target.X, current.Target.Y, new bool[_H, _W], current.CollectedKeys, _reachableTargets);

                Parallel.ForEach(_reachableTargets, target =>
                {
                    var newGrid = CloneGrid(current.Grid);
                    var steps = distances[current.Target.Value][target.Value];
                    if (char.IsUpper(target.Value))
                    {
                        State state = new State()
                        {
                            KeyCount = current.KeyCount,
                            Grid = newGrid,
                            Steps = current.Steps + steps,
                            CollectedKeys = new List<Target>(current.CollectedKeys.Select(k => k.Clone())),
                            Target = target
                        };
                        state.Grid[state.Target.X][state.Target.Y] = (char)Area.FLOOR;
                        state.CollectedKeys.Remove(target);
                        queue.Add(-state.KeyCount, state);
                    }
                    else
                    {
                        State state = new State()
                        {
                            KeyCount = current.KeyCount + 1,
                            Grid = newGrid,
                            Steps = current.Steps + steps,
                            CollectedKeys = new List<Target>(current.CollectedKeys.Select(k => k.Clone())),
                            Target = target
                        };
                        state.Grid[state.Target.X][state.Target.Y] = (char)Area.FLOOR;
                        state.CollectedKeys.Add(target);
                        queue.Add(-state.KeyCount, state);
                    }
                });

                if (runs % 2500 == 0) Console.WriteLine($"Ran a total of {runs} times -- Best path found: {ans} (Must be lower than: 5976) -- Completed paths: {completedPaths} -- " +
                    $"Paths left to search: {queue.Count}");
                runs++;
            }

            Console.WriteLine("Lower than: 5976");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void CalculateReachableTargets(char[][] grid, int x, int y, bool[,] visisted, List<Target> collectedKeys, List<Target> reachableTargets)
        {
            if (x < 0 || x >= grid.Length || y < 0 || y >= grid[x].Length || visisted[x, y] || grid[x][y] == (char)Area.WALL) return;

            if (char.IsUpper(grid[x][y]))
            {
                if (collectedKeys.Any(k => k.Value == char.ToLower(grid[x][y])))
                {
                    reachableTargets.Add(new Target()
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
                reachableTargets.Add(new Target()
                {
                    Value = grid[x][y],
                    X = x,
                    Y = y
                });
                return;
            }

            visisted[x, y] = true;

            CalculateReachableTargets(grid, x + 1, y, visisted, collectedKeys, reachableTargets);
            CalculateReachableTargets(grid, x - 1, y, visisted, collectedKeys, reachableTargets);
            CalculateReachableTargets(grid, x, y + 1, visisted, collectedKeys, reachableTargets);
            CalculateReachableTargets(grid, x, y - 1, visisted, collectedKeys, reachableTargets);
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
            char[][] result = new char[_H][];

            for (int i = 0; i < _H; i++)
            {
                result[i] = new char[_W];
                for (int j = 0; j < _W; j++)
                {
                    result[i][j] = grid[i][j];
                }
            }

            return result;
        }

        private int CalculateShortestPath(int x, int y, int targetX, int targetY, char[][] grid)
        {
            Tile start = new Tile()
            {
                X = x,
                Y = y,
                Distance = CalculateManhattenDistance(x, y, targetX, targetY)
            };

            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, Tile>(comparer);
            bool[,] visisted = new bool[_H, _W];

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

                if (next.X >= 0 && next.X < _H && next.Y >= 0 && next.Y < _W && grid[next.X][next.Y] != (char)Area.WALL)
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
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2019\Day18\sample.txt";
            var lines = File.ReadAllLines(path);
            _H = lines.Length;
            _W = lines.First().Length;
            _grid = new char[_H][];
            for (int i = 0; i < _H; i++)
            {
                _grid[i] = lines[i].ToCharArray();
            }

            for (int i = 0; i < _H; i++)
            {
                for (int j = 0; j < _W; j++)
                {
                    char c = _grid[i][j];
                    if (char.IsLower(c))
                    {
                        _keyCount++;
                        _targets.Add(new Target() { Value = c, X = i, Y = j });
                    }
                    else if (char.IsUpper(c))
                    {
                        _targets.Add(new Target() { Value = c, X = i, Y = j });
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
