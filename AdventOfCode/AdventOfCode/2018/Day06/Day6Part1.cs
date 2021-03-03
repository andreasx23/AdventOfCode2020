using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day06
{
    public class Day6Part1
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
            char[][] grid = new char[width][];
            for (int i = 0; i < height; i++)
            {
                grid[i] = new char[height];
            }

            Queue<Tile> queue = new Queue<Tile>();
            bool[,] isVisited = new bool[width, height];
            foreach (var tile in tiles)
            {
                grid[tile.X][tile.Y] = tile.Value;
                queue.Enqueue(tile);
                isVisited[tile.X, tile.Y] = true;
            }

            while (queue.Any())
            {
                var current = queue.Dequeue();

                List<Tile> adjecentTiles = WalkableAdjecentTiles(grid, current, isVisited, false);
                foreach (var tile in adjecentTiles)
                {
                    if (!isVisited[tile.X, tile.Y])
                    {
                        isVisited[tile.X, tile.Y] = true;
                        queue.Enqueue(tile);
                        grid[tile.X][tile.Y] = tile.Value;
                    }
                }
            }

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    int minDistance = int.MaxValue;
                    char minTileValue = '#';
                    foreach (var tile in tiles)
                    {
                        var distance = CalculateManhattenDistance(tile.X, x, tile.Y, y);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minTileValue = tile.Value;
                        }
                    }

                    foreach (var tile in tiles)
                    {
                        if (tile.Value == minTileValue) continue;

                        var equalDistance = CalculateManhattenDistance(tile.X, x, tile.Y, y);
                        if (minDistance == equalDistance)
                        {
                            grid[x][y] = '.';
                        }
                    }
                }
            }

            //Print(grid);

            long ans = 0;
            foreach (var tile in tiles)
            {
                Console.WriteLine("Checking: " + tile.Value);                
                Queue<Tile> tempQueue = new Queue<Tile>();
                tempQueue.Enqueue(tile);
                bool[,] _isVisited = new bool[width, height];
                _isVisited[tile.X, tile.Y] = true;

                long sum = 1;
                while (tempQueue.Any())
                {
                    var current = tempQueue.Dequeue();

                    List<Tile> adjecentTiles = WalkableAdjecentTiles(grid, current, _isVisited, true);
                    if (adjecentTiles.Count == 0) //Not valid
                    {
                        sum = 0;
                        break;
                    }
                    else
                    {
                        foreach (var adTile in adjecentTiles)
                        {
                            if (!_isVisited[adTile.X, adTile.Y])
                            {
                                _isVisited[adTile.X, adTile.Y] = true;

                                if (char.ToLower(tile.Value) == adTile.Value)
                                {
                                    tempQueue.Enqueue(adTile);
                                    sum++;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(sum);
                ans = Math.Max(ans, sum);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Tile> WalkableAdjecentTiles(char[][] grid, Tile current, bool[,] isVisited, bool isAnswer)
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

                if (!isAnswer)
                {
                    tile.Value = char.ToLower(current.Value);

                    if (tile.X < 0 || tile.X >= grid.Length || tile.Y < 0 || tile.Y >= grid[0].Length || isVisited[tile.X, tile.Y]) //Boundary check
                    {
                        continue;
                    }
                }
                else
                {
                    if (tile.X < 0 || tile.X >= grid.Length || tile.Y < 0 || tile.Y >= grid[0].Length) //Boundary check
                    {
                        return new List<Tile>();
                    }

                    tile.Value = grid[tile.X][tile.Y];
                    if (isVisited[tile.X, tile.Y])
                    {
                        continue;
                    }
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
            var lines = File.ReadAllLines(path).Select(s => s.Split(',').Select(s => s.Trim()).Select(int.Parse).ToList()).ToList();

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
