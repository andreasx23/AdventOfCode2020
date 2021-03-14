using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day15
{
    public class Day15Part1
    {
        private char[][] grid;
        private int H = 0, W = 0;

        //https://leetcode.com/problems/shortest-path-in-binary-matrix/discuss/1065411/c-solution-using-a
        //https://adventofcode.com/2018/day/15
        private void Day15()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Unit> units = new List<Unit>();
            int ids = 0;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (grid[i][j] == 'E' || grid[i][j] == 'G')
                    {
                        units.Add(new Unit()
                        {
                            X = i,
                            Y = j,
                            Type = grid[i][j],
                            ID = ids++
                        });
                    }
                }
            }

            Print(grid);

            bool isFinished = false;
            int rounds = 0;
            while (!isFinished)
            {
                foreach (var current in units.OrderBy(t => t.X).ThenBy(t => t.Y))
                {
                    if (current.HP <= 0) continue;

                    var (isInRange, Enemy) = IsRangeOfEnemy(grid, current);
                    if (isInRange)
                    {
                        var enemy = units.FirstOrDefault(t => t.X == Enemy.X && t.Y == Enemy.Y);

                        if (enemy == null)
                        {
                            continue;
                        }

                        enemy.HP -= current.Attack;

                        if (enemy.HP <= 0)
                        {
                            grid[enemy.X][enemy.Y] = '.';
                            units.Remove(enemy);
                        }

                        if (units.Any(t => t.Type == enemy.Type))
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"THE {enemy.Type} LOST!");
                            isFinished = true;
                            break;
                        }
                    }

                    List<Unit> targets = new List<Unit>();
                    foreach (var possibleTarget in units)
                    {
                        if (current.Type == possibleTarget.Type) continue;

                        var target = GreedyBFS(grid, current, possibleTarget, rounds);
                        if (target != null)
                        {
                            targets.Add(target);
                        }
                    }

                    if (targets.Count > 0)
                    {
                        var position = targets.OrderBy(t => t.Distance).ThenBy(t => t.X).ThenBy(t => t.Y).First();

                        grid[current.X][current.Y] = '.';
                        current.X = position.X;
                        current.Y = position.Y;
                        grid[current.X][current.Y] = current.Type;

                        (isInRange, Enemy) = IsRangeOfEnemy(grid, current);
                        if (isInRange)
                        {
                            var enemy = units.FirstOrDefault(t => t.X == Enemy.X && t.Y == Enemy.Y);

                            if (enemy == null)
                            {
                                continue;
                            }

                            enemy.HP -= current.Attack;

                            if (enemy.HP <= 0)
                            {
                                grid[enemy.X][enemy.Y] = '.';
                                units.Remove(enemy);
                            }

                            if (units.Any(t => t.Type == enemy.Type))
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine($"THE {enemy.Type} LOST!");
                                isFinished = true;
                                break;
                            }
                        }
                    }
                }

                if (!isFinished)
                {
                    rounds++;
                    //Console.WriteLine("Round: " + rounds);
                    //Print(grid);
                }
            }

            Console.WriteLine("Expected: 20 rounds 937 total HP = 18740");

            Print(grid);
            
            var hp = units.Sum(t => t.HP);
            Console.WriteLine("Rounds: " + rounds + " HP: " + hp);

            Console.WriteLine("Answers is smaller than 121 rounds and 280599 pts");
            long ans = rounds * hp;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Unit> WalkableCoordinates(char[][] grid, Unit current)
        {
            List<Unit> coordinates = new List<Unit>()
            {
                new Unit(){ X = -1, Y = 0 },
                new Unit(){ X = +1, Y = 0 },
                new Unit(){ X = 0, Y = -1 },
                new Unit(){ X = 0, Y = +1 },
            };

            List<Unit> walkableCoordiantes = new List<Unit>();
            foreach (var location in coordinates)
            {
                location.X += current.X;
                location.Y += current.Y;

                if (location.X < 0 || location.X >= grid.Length || location.Y < 0 || location.Y >= grid[0].Length || grid[location.X][location.Y] != '.') continue;

                walkableCoordiantes.Add(location);
            }

            return walkableCoordiantes;
        }

        private (bool isInRange, Unit Enemy) IsRangeOfEnemy(char[][] grid, Unit current)
        {
            List<Unit> coordinates = new List<Unit>()
            {
                new Unit(){ X = -1, Y = 0 },
                new Unit(){ X = +1, Y = 0 },
                new Unit(){ X = 0, Y = -1 },
                new Unit(){ X = 0, Y = +1 },
            };

            List<Unit> enemiesInRange = new List<Unit>();
            foreach (var location in coordinates)
            {
                location.X += current.X;
                location.Y += current.Y;                

                if (location.X < 0 || location.X >= grid.Length || location.Y < 0 || location.Y >= grid[0].Length || grid[location.X][location.Y] == '#' || grid[location.X][location.Y] == '.') continue;

                if (current.Type != grid[location.X][location.Y])
                {
                    enemiesInRange.Add(location);
                }
            }

            if (enemiesInRange.Count > 0)
            {
                return (true, enemiesInRange.OrderBy(t => t.HP).ThenBy(t => t.X).ThenBy(t => t.Y).First());
            }

            return (false, null);
        }

        private void Print(char[][] grid)
        {
            foreach (var values in grid)
            {
                Console.WriteLine(string.Join(" ", values));
            }

            Console.WriteLine();
        }

        private int CalculateManhattenDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
        }

        public Unit GreedyBFS(char[][] grid, Unit start, Unit target, int rounds)
        {
            start.Cost = 0;
            start.Distance = CalculateManhattenDistance(start.X, start.Y, target.X, target.Y);
            start.Priority = 0;
            start.Parent = null;

            bool[,] isVisited = new bool[H, W];
            Queue<Unit> queue = new Queue<Unit>();
            queue.Enqueue(start);
            isVisited[start.X, start.Y] = true;

            List<Unit> bestPositions = new List<Unit>();
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Distance == 1)
                {
                    Unit temp = current;
                    List<Unit> output = new List<Unit>();
                    while (temp.Parent != null)
                    {
                        output.Add(temp);
                        temp = temp.Parent;
                    }
                    bestPositions.Add(output.Last());
                }

                var walkable = WalkableCoordinates(grid, current);
                List<Unit> nextCoordinates = new List<Unit>();
                foreach (var next in walkable)
                {
                    if (!isVisited[next.X, next.Y])
                    {
                        isVisited[next.X, next.Y] = true;
                        next.Parent = current;
                        next.Cost = current.Cost + 1;
                        next.Distance = CalculateManhattenDistance(next.X, next.Y, target.X, target.Y);
                        next.Type = current.Type;
                        nextCoordinates.Add(next);
                    }
                }

                if (nextCoordinates.Count > 0)
                {
                    foreach (var next in nextCoordinates.OrderBy(t => t.Distance).ThenBy(t => t.X).ThenBy(t => t.Y))
                    {
                        queue.Enqueue(next);
                    }
                }
            }

            return bestPositions.Count > 0 ? bestPositions.OrderBy(t => t.X).ThenBy(t => t.Y).First() : null;
        }

        private Unit AStar(char[][] grid, Unit start, Unit target)
        {
            Dictionary<Unit, Unit> cameFrom = new Dictionary<Unit, Unit>();
            Dictionary<Unit, int> costSoFar = new Dictionary<Unit, int>();

            start.Cost = 0;
            start.Distance = CalculateManhattenDistance(start.X, start.Y, target.X, target.Y);
            start.Priority = 0;
            start.Parent = null;

            HashSet<(int X, int Y)> isVisited = new HashSet<(int X, int Y)>();
            PriorityQueue<Unit> queue = new PriorityQueue<Unit>();
            queue.Enqueue(start);
            cameFrom[start] = start;
            costSoFar[start] = 0;
            isVisited.Add((start.X, start.Y));

            List<Unit> bestPositions = new List<Unit>();
            while (queue.Count() > 0)
            {
                var current = queue.Dequeue();

                if (current.Distance == 1)
                {
                    Unit temp = current;
                    List<Unit> output = new List<Unit>();
                    while (temp.Parent != null)
                    {
                        output.Add(temp);
                        temp = temp.Parent;
                    }

                    var last = output.Last();
                    if (bestPositions.Contains(last))
                    {
                        return bestPositions.OrderBy(t => t.X).ThenBy(t => t.Y).First();
                    }
                    else
                    {
                        bestPositions.Add(last);
                    }
                }

                var walkable = WalkableCoordinates(grid, current);
                foreach (var next in walkable)
                {
                    int newCost = costSoFar[current] + (isVisited.Contains((next.X, next.Y)) ? 5 : 1);
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        isVisited.Add((next.X, next.Y));

                        int distance = CalculateManhattenDistance(next.X, next.Y, target.X, target.Y);
                        int priority = newCost + distance;
                        next.Parent = current;
                        next.Cost = newCost;
                        next.Distance = distance;
                        next.Priority = priority;

                        costSoFar[next] = newCost;
                        cameFrom[next] = current;
                        queue.Enqueue(next);
                    }
                }
            }

            return null;
        }

        private void ReadData()
        {
            const string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day15\sample2.txt";
            var lines = File.ReadAllLines(path);

            H = lines.Length;
            W = lines[0].Length;
            grid = new char[H][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }
        }

        public void TestCase()
        {
            ReadData();
            Day15();
        }
    }
}
