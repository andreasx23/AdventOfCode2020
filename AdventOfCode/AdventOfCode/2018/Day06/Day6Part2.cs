using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day06
{
    public class Day6Part2
    {
        class Tile
        {
            public char Value;
            public int X;
            public int Y;
        }

        private readonly List<Tile> tiles = new List<Tile>();

        private void Day6()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int size = 1000;
            int width = size, height = size;
            char[][] grid = new char[height][];
            for (int i = 0; i < height; i++)
            {
                grid[i] = new char[width];

                for (int j = 0; j < width; j++)
                {
                    grid[i][j] = '.';
                }
            }

            foreach (var tile in tiles)
            {
                grid[tile.X][tile.Y] = tile.Value;
            }

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    int sum = 0;
                    foreach (var tile in tiles)
                    {
                        sum += CalculateManhattenDistance(tile.X, x, tile.Y, y);
                    }

                    if (sum < 10000 && grid[x][y] == '.')
                    {
                        grid[x][y] = '#';
                    }
                }
            }

            Queue<Tile> queue = new Queue<Tile>();
            bool[,] isVisited = new bool[height, width];
            foreach (var tile in tiles)
            {
                List<Tile> adjecentTiles = WalkableAdjecentTiles(grid, tile, isVisited);

                foreach (var _tile in adjecentTiles)
                {
                    if (grid[_tile.X][_tile.Y] == '#')
                    {
                        queue.Enqueue(tile);
                        isVisited[tile.X, tile.Y] = true;
                        break;
                    }
                }
            }

            long ans = queue.Count;
            while (queue.Any())
            {
                var current = queue.Dequeue();

                List<Tile> adjecentTiles = WalkableAdjecentTiles(grid, current, isVisited);
                foreach (var tile in adjecentTiles)
                {
                    if (!isVisited[tile.X, tile.Y])
                    {
                        isVisited[tile.X, tile.Y] = true;

                        if (grid[tile.X][tile.Y] == '#')
                        {
                            ans++;
                            queue.Enqueue(tile);
                        }
                    }
                }
            }

            //Print(grid);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Tile> WalkableAdjecentTiles(char[][] grid, Tile current, bool[,] isVisited)
        {
            List<Tile> manhattenCoordinates = new List<Tile>()
            {
                new Tile() { X = -1, Y = 0 }, //Up
                new Tile() { X = +1, Y = 0 }, //Down
                new Tile() { X = 0, Y = -1 }, //Left
                new Tile() { X = 0, Y = +1 }, //Right
            };

            List<Tile> walkable = new List<Tile>();
            foreach (var tile in manhattenCoordinates)
            {
                tile.X += current.X;
                tile.Y += current.Y;

                if (tile.X < 0 || tile.X >= grid.Length || tile.Y < 0 || tile.Y >= grid[0].Length || isVisited[tile.X, tile.Y]) //Boundary check
                {
                    continue;
                }

                walkable.Add(tile);
            }

            return walkable;
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join(" ", item));
            }
        }

        private int CalculateManhattenDistance(int X1, int X2, int Y1, int Y2)
        {
            return Math.Abs(X2 - X1) + Math.Abs(Y2 - Y1);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day06\input.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(',').Select(s => int.Parse(s.Trim())).ToList()).ToList();

            for (int i = 0; i < lines.Count(); i++)
            {
                tiles.Add(new Tile()
                {
                    X = lines[i][1],
                    Y = lines[i][0],
                    Value = Convert.ToChar(i + 'A')
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
