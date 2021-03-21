using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day13
{
    public class Day13Part2
    {
        private static readonly bool isSample = false;
        private readonly int input = (isSample) ? 10 : 1350;

        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int width = (isSample) ? 7 : 200, height = (isSample) ? 10 : 200;
            char[][] grid = new char[width][];
            for (int y = 0; y < width; y++)
            {
                grid[y] = new char[height];
                for (int x = 0; x < height; x++)
                {
                    int sum = x * x + 3 * x + 2 * x * y + y + y * y + input;

                    if (Convert.ToString(sum, 2).Count(c => c == '1') % 2 == 0)
                    {
                        grid[y][x] = '.';
                    }
                    else
                    {
                        grid[y][x] = '#';
                    }
                }
            }

            Queue<Tile> queue = new Queue<Tile>();
            Tile start = new Tile() { X = 1, Y = 1, Cost = 0 };
            queue.Enqueue(start);
            bool[,] isVisited = new bool[width, height];
            isVisited[start.X, start.Y] = true;

            int ans = 1;
            while (queue.Any())
            {
                Tile current = queue.Dequeue();

                var walkable = Walkable(grid, current);
                foreach (var next in walkable)
                {
                    if (!isVisited[next.X, next.Y])
                    {
                        isVisited[next.X, next.Y] = true;
                        next.Parent = current;
                        next.Cost = current.Cost + 1;

                        if (next.Cost <= 50)
                        {
                            ans++;
                            queue.Enqueue(next);
                        }
                    }
                }
            }

            //Print(grid);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Tile> Walkable(char[][] grid, Tile current)
        {
            List<Tile> neighbours = new List<Tile>()
            {
                new Tile() { X = -1, Y = 0 },
                new Tile() { X = 1, Y = 0 },
                new Tile() { X = 0, Y = -1 },
                new Tile() { X = 0, Y = 1 }
            };

            List<Tile> result = new List<Tile>();
            foreach (var next in neighbours)
            {
                next.X += current.X;
                next.Y += current.Y;

                if (next.X < 0 || next.X >= grid.Length || next.Y < 0 || next.Y >= grid[0].Length || grid[next.X][next.Y] == '#')
                {
                    continue;
                }

                result.Add(next);
            }

            return result;
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day08\input.txt";
            //var lines = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day13();
        }
    }
}
