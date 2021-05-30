using AlgoKit.Collections.Heaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day18
{
    public class Day18Part2
    {
        private char[][] _grid;
        private int _H, _W;
        private int _keyCount = 0;
        private readonly List<Target> _targets = new List<Target>();
        private readonly Dictionary<char, List<(char target, int distance, HashSet<char> doors, HashSet<char> keys)>> _distances = new Dictionary<char, List<(char target, int distance, HashSet<char> doors, HashSet<char> keys)>>();

        /// <summary>
        /// Bruteforce algorithm -- I have no idea how to optimize my algorithm further
        /// My answer was: 
        /// </summary>
        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<(int x, int y, char id)> robots = new List<(int x, int y, char id)>();
            int id = 0;
            for (int i = 0; i < _H; i++)
            {
                for (int j = 0; j < _W; j++)
                {
                    if (_grid[i][j] == (char)Area.ENTRANCE)
                    {
                        char num = id.ToString().First();
                        robots.Add((i, j, num));
                        id++;
                    }
                }
            }

            foreach (var robot in robots)
            {
                List<Target> targets = new List<Target>();
                CalculateAllTargetsInRegion(_grid, robot.x, robot.y, new bool[_H, _W], targets);

                _distances.Add(robot.id, new List<(char target, int distance, HashSet<char> doors, HashSet<char> keys)>());
                foreach (var target in targets.Where(t => char.IsLower(t.Value)))
                {
                    var (distance, doors, keys) = CalculateShortestPath(robot.x, robot.y, target.X, target.Y, _grid);
                    _distances[robot.id].Add((target.Value, distance, doors, keys));
                }

                foreach (var me in targets.Where(t => char.IsLower(t.Value)))
                {
                    _distances.Add(me.Value, new List<(char target, int distance, HashSet<char> doors, HashSet<char> keys)>());
                    foreach (var target in targets.Where(t => char.IsLower(t.Value)))
                    {
                        if (me.Value == target.Value) continue;
                        var (distance, doors, keys) = CalculateShortestPath(me.X, me.Y, target.X, target.Y, _grid);
                        _distances[me.Value].Add((target.Value, distance, doors, keys));
                    }
                }
            }

            int ans = int.MaxValue, totalRuns = 0;
            for (int i = 0; i < 100; i++)
            {
                var queue = GeneratePriorityQueue(robots);

                int runs = 0, completedPaths = 0;
                Random rand = new Random();
                while (queue.Any() && runs < 10_000_000)
                {
                    var current = queue.Pop().Value;

                    if (current.KeyCount == _keyCount)
                    {
                        ans = Math.Min(ans, current.Steps);
                        completedPaths++;
                        continue;
                    }

                    foreach (var robot in robots)
                    {
                        RunSearch(current, robot.id, ref queue, rand);
                    }
                    RunSearch(current, current.Target, ref queue, rand);

                    if (runs % 30000 == 0) Console.WriteLine($"Current run: {i} -- Ran a total of {totalRuns + runs} times -- Best path found: {ans} -- Completed paths this run: {completedPaths} -- Paths left to search this run: {queue.Count}");
                    runs++;
                }
                totalRuns += runs;
            }

            Console.WriteLine("Lower than: 2592");
            Console.WriteLine("Wrong answer: 2578");
            Console.WriteLine("Wrong answer: 2552");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void RunSearch(State current, char id, ref PairingHeap<int, State> queue, Random rand)
        {
            var targets = _distances[id].Where(kv => !current.CollectedKeys.Contains(kv.target) &&
                                                (kv.doors.Count == 0 || kv.doors.All(current.CollectedKeys.Contains)));
            foreach (var target in targets)
            {
                State state = new State()
                {
                    KeyCount = current.KeyCount + 1,
                    Steps = current.Steps + target.distance,
                    CollectedKeys = new HashSet<char>(current.CollectedKeys) { target.target },
                    Target = target.target
                };

                foreach (var key in target.keys)
                {
                    if (state.CollectedKeys.Add(key))
                    {
                        state.KeyCount++;
                    }
                }

                queue.Add(rand.Next(0, 50), state);
            }
        }

        private PairingHeap<int, State> GeneratePriorityQueue(List<(int x, int y, char id)> robots)
        {
            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, State>(comparer);

            foreach (var robot in robots)
            {
                List<Target> reachableTargets = new List<Target>();
                CalculateReachableTargets(_grid, robot.x, robot.y, new bool[_H, _W], new List<Target>(), reachableTargets);

                foreach (var target in reachableTargets.Where(t => char.IsLower(t.Value)))
                {
                    var state = new State()
                    {
                        Steps = _distances[robot.id].First(kv => kv.target == target.Value).distance,
                        KeyCount = 1,
                        CollectedKeys = new HashSet<char>() { target.Value },
                        Target = target.Value
                    };
                    queue.Add(-state.KeyCount, state);
                }
            }

            return queue;
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

        private void CalculateAllTargetsInRegion(char[][] grid, int x, int y, bool[,] visisted, List<Target> targets)
        {
            if (x < 0 || x >= grid.Length || y < 0 || y >= grid[x].Length || visisted[x, y] || grid[x][y] == (char)Area.WALL) return;

            if (char.IsUpper(grid[x][y]) || char.IsLower(grid[x][y]))
            {
                targets.Add(new Target()
                {
                    Value = grid[x][y],
                    X = x,
                    Y = y
                });
            }

            visisted[x, y] = true;

            CalculateAllTargetsInRegion(grid, x + 1, y, visisted, targets);
            CalculateAllTargetsInRegion(grid, x - 1, y, visisted, targets);
            CalculateAllTargetsInRegion(grid, x, y + 1, visisted, targets);
            CalculateAllTargetsInRegion(grid, x, y - 1, visisted, targets);
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private (int distance, HashSet<char> doors, HashSet<char> keys) CalculateShortestPath(int x, int y, int targetX, int targetY, char[][] grid)
        {
            Tile start = new Tile()
            {
                X = x,
                Y = y,
                Distance = CalculateManhattenDistance(x, y, targetX, targetY)
            };

            var comparer = Comparer<int>.Default;
            var queue = new PairingHeap<int, (Tile tile, HashSet<char> doors, HashSet<char> keys)>(comparer);
            bool[,] visisted = new bool[_H, _W];

            queue.Add(start.CostDistance, (start, new HashSet<char>(), new HashSet<char>()));
            visisted[start.X, start.Y] = true;

            Dictionary<(int x, int y), int> map = new Dictionary<(int x, int y), int>
            {
                { (start.X, start.Y), start.CostDistance }
            };

            while (!queue.IsEmpty)
            {
                var current = queue.Pop().Value;

                if (current.tile.X == targetX && current.tile.Y == targetY)
                {
                    return (current.tile.Cost, current.doors, current.keys);
                }

                foreach (var next in ValidTiles(current.tile, grid))
                {
                    next.Cost = current.tile.Cost + 1;
                    next.Distance = CalculateManhattenDistance(next.X, next.Y, targetX, targetY);

                    if (map.TryGetValue((next.X, next.Y), out int CostDistance))
                    {
                        if (CostDistance > next.CostDistance)
                        {
                            map[(next.X, next.Y)] = next.CostDistance;
                            queue.Add(next.CostDistance, (next, current.doors, current.keys));
                        }
                    }
                    else if (!visisted[next.X, next.Y])
                    {
                        HashSet<char> doors = new HashSet<char>(current.doors);
                        HashSet<char> keys = new HashSet<char>(current.keys);
                        if (char.IsUpper(grid[next.X][next.Y]))
                        {
                            doors.Add(char.ToLower(grid[next.X][next.Y])); //Converting door to key
                        }
                        else if (char.IsLower(grid[next.X][next.Y]))
                        {
                            keys.Add(grid[next.X][next.Y]);
                        }

                        visisted[next.X, next.Y] = true;
                        queue.Add(next.CostDistance, (next, doors, keys));
                        map.Add((next.X, next.Y), next.CostDistance);
                    }
                }
            }

            return (-1, null, null);
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
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day18\input2.txt";
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
