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
        private class Tile
        {
            public int ID;
            public int X;
            public int Y;
            public char Value;
            public int Attack;
            public int HP;

            //A*
            public int Cost;
            public int Distance;
            public int CostDistance => Cost + Distance;
            public Tile Parent;
            
            public Tile()
            {
                Attack = 3;
                HP = 200;
            }
        }

        private char[][] grid;
        private int H = 0, W = 0;

        //https://leetcode.com/problems/shortest-path-in-binary-matrix/discuss/1065411/c-solution-using-a
        //https://adventofcode.com/2018/day/15
        private void Day15()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Tile> tiles = new List<Tile>();
            int ids = 0;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (grid[i][j] == 'E' || grid[i][j] == 'G')
                    {
                        tiles.Add(new Tile()
                        {
                            X = i,
                            Y = j,
                            Value = grid[i][j],
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
                if (rounds % 50 == 0)
                {
                    Console.WriteLine("Rounds: " + rounds);                    
                    foreach (var item in tiles.OrderBy(t => t.Value))
                    {
                        Console.WriteLine("NPC: " + item.ID + " " + item.Value + " " + item.HP);
                    }
                    Print(grid);
                }

                var ordered = tiles.OrderBy(t => t.X).ThenBy(t => t.Y);
                foreach (var current in ordered)
                {
                    var (isInRange, Enemy) = IsRangeOfEnemy(grid, current);
                    if (isInRange)
                    {
                        var enemy = tiles.First(t => t.X == Enemy.X && t.Y == Enemy.Y);
                        enemy.HP -= current.Attack;

                        if (enemy.HP <= 0)
                        {
                            grid[enemy.X][enemy.Y] = '.';
                            tiles.Remove(enemy);
                        }

                        if (tiles.Any(t => t.Value == enemy.Value))
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"THE {enemy.Value} LOST!");
                            isFinished = true;
                            break;
                        }
                    }

                    List<Tile> stepsTowardClosestTarget = new List<Tile>();
                    int distanceToClosestTarget = int.MaxValue;
                    foreach (var possiblePossition in WalkablePositions(grid, current))
                    {
                        foreach (var target in ordered)
                        {
                            if (current.ID == target.ID || current.Value == target.Value) continue;

                            Tile output = AStar(grid, current, target);
                            if (output != null)
                            {
                                if (output.CostDistance < distanceToClosestTarget)
                                {
                                    stepsTowardClosestTarget = new List<Tile>() { output };
                                    distanceToClosestTarget = output.CostDistance;
                                }
                                else if (output.CostDistance == distanceToClosestTarget)
                                {
                                    stepsTowardClosestTarget.Add(output);
                                }
                            }
                        }
                    }

                    if (stepsTowardClosestTarget.Count > 0)
                    {
                        Tile position = stepsTowardClosestTarget.OrderBy(t => t.X).ThenBy(t => t.Y).First();
                        grid[current.X][current.Y] = '.';
                        current.X = position.X;
                        current.Y = position.Y;
                        grid[current.X][current.Y] = current.Value;
                    }
                }

                if (!isFinished)
                {
                    //Print(grid);
                    tiles = ordered.ToList();
                    rounds++;
                }
            }

            Console.WriteLine("Expected: 20 rounds 937 total HP = 18740");

            Print(grid);

            long ans = rounds * tiles.Sum(t => t.HP);
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Tile> WalkablePositions(char[][] grid, Tile current)
        {
            List<Tile> coordinates = new List<Tile>()
            {
                new Tile(){ X = -1, Y = 0 },
                new Tile(){ X = +1, Y = 0 },
                new Tile(){ X = 0, Y = -1 },
                new Tile(){ X = 0, Y = +1 },
            };

            List<Tile> result = new List<Tile>();
            foreach (var tile in coordinates)
            {
                tile.X += current.X;
                tile.Y += current.Y;

                if (tile.X < 0 || tile.X >= grid.Length || tile.Y < 0 || tile.Y >= grid[0].Length || 
                    grid[tile.X][tile.Y] == '#' || grid[tile.X][tile.Y] == current.Value) continue;

                result.Add(tile);
            }

            return result;
        }

        private (bool isInRange, Tile Enemy) IsRangeOfEnemy(char[][] grid, Tile current)
        {
            List<Tile> coordinates = new List<Tile>()
            {
                new Tile(){ X = -1, Y = 0 },
                new Tile(){ X = +1, Y = 0 },
                new Tile(){ X = 0, Y = -1 },
                new Tile(){ X = 0, Y = +1 },
            };

            foreach (var tile in coordinates)
            {
                tile.X += current.X;
                tile.Y += current.Y;

                if (tile.X < 0 || tile.X >= grid.Length || tile.Y < 0 || tile.Y >= grid[0].Length) continue;

                if (current.Value == 'E' && grid[tile.X][tile.Y] == 'G' || current.Value == 'G' && grid[tile.X][tile.Y] == 'E')
                {
                    return (true, tile);
                }
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

        private Tile AStar(char[][] grid, Tile start, Tile target)
        {
            bool[,] isVisited = new bool[H, W];
            Queue<Tile> queue = new Queue<Tile>();
            start.Cost = 1;
            start.Parent = null;
            start.Distance = CalculateManhattenDistance(start.X, start.Y, target.X, target.Y);
            queue.Enqueue(start);
            isVisited[start.X, start.Y] = true;

            List<Tile> ans = new List<Tile>();
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.X == target.X && current.Y == target.Y)
                {
                    var temp = current;
                    List<Tile> output = new List<Tile>();
                    while (temp.Parent != null)
                    {
                        output.Add(temp);
                        temp = temp.Parent;
                    }
                    ans.Add(output.Last());
                }

                var walkable = WalkablePositions(grid, current);
                foreach (var tile in walkable)
                {
                    if (!isVisited[tile.X, tile.Y])
                    {
                        isVisited[tile.X, tile.Y] = true;
                        tile.Cost = current.Cost + 1;
                        current.Distance = CalculateManhattenDistance(tile.X, tile.Y, target.X, target.Y);
                        tile.Parent = current;
                        queue.Enqueue(tile);
                    }
                }
            }
            return ans.Count > 0 ? ans.OrderBy(t => t.X).ThenBy(t => t.Y).First() : null;
        }

        private void ReadData()
        {
            const string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2018\Day15\sample2.txt";
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
