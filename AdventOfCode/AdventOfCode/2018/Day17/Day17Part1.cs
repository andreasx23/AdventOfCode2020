using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day17
{
    public class Day17Part1
    {
        private class Tile
        {
            public int X;
            public int Y;
            public CellValue Value;
        }

        private List<Tile> tiles = new List<Tile>();
        private readonly Tile well = new Tile() { X = 0, Y = 50, Value = CellValue.Well }; //0 500 +
        private static int minY = int.MaxValue, maxY = int.MinValue, minX = int.MaxValue, maxX = int.MinValue, H = maxX - minX, W = maxY - minY;

        private enum CellValue
        {
            Sand = '.',
            Clay = '#',
            WaterDropping = '|',
            WaterFlowing = '~',
            Well = '+',
        }

        //https://adventofcode.com/2018/day/17
        //Cannot solve it
        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            H = 25;
            W = 75;

            CellValue[][] grid = new CellValue[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = new CellValue[W];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = CellValue.Sand;
                }
            }

            grid[well.X][well.Y] = well.Value;

            foreach (var tile in tiles)
            {
                if (tile.X > 50)
                {
                    tile.X -= 450;
                }

                if (tile.Y > 50)
                {
                    tile.Y -= 450;
                }

                grid[tile.X][tile.Y] = tile.Value;
            }

            Queue<Tile> queue = new Queue<Tile>();

            int tempX = well.X + 1, tempY = well.Y;
            while (grid[tempX][tempY] != CellValue.Clay)
            {
                Tile tile = new Tile()
                {
                    X = tempX,
                    Y = tempY,
                    Value = CellValue.WaterDropping
                };
                grid[tempX][tempY] = tile.Value;
                tiles.Add(tile);

                if (grid[tempX][tempY + 1] == CellValue.Clay || grid[tempX][tempY - 1] == CellValue.Clay)
                {
                    queue.Enqueue(tile);
                }

                tempX++;
            }

            while (queue.Any())
            {
                Tile current = queue.Dequeue();

                grid[current.X][current.Y] = current.Value;

                int newX = current.X, newY = current.Y - 1;
                if (grid[newX][newY] != CellValue.Clay)
                {
                    Tile tile = new Tile()
                    {
                        X = newX,
                        Y = newY,
                        Value = CellValue.WaterFlowing
                    };
                    queue.Enqueue(tile);                    
                }
            }

            Print(grid);

            int ans = 0;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print(CellValue[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item.Select(c => (char)c)));
            }
        }

        private void ReadData()
        {
            const string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2018\Day17\sample.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                Tile current = new Tile();
                var split = s.Split(',').Select(_ => _.Trim()).ToList();
                var leftPart = split[0].Split('=');
                var rightPart = split[1].Split('=');

                var rightPartValues = rightPart[1].Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
                int min = int.Parse(rightPartValues.First()), max = int.Parse(rightPartValues.Last());
                if (rightPart[0][0] == 'y')
                {
                    for (int i = min; i <= max; i++)
                    {
                        Tile temp = new Tile
                        {
                            X = i,
                            Y = int.Parse(leftPart.Last()),
                            Value = CellValue.Clay
                        };

                        tiles.Add(temp);
                    }
                }
                else
                {
                    for (int i = min; i <= max; i++)
                    {
                        Tile temp = new Tile
                        {
                            X = int.Parse(leftPart.Last()),
                            Y = i,
                            Value = CellValue.Clay
                        };

                        tiles.Add(temp);
                    }
                }
            }

            tiles.Add(well); //well (0, 500, +)

            tiles = tiles.Distinct().ToList();
            minY = tiles.Min(t => t.Y);
            maxY = tiles.Max(t => t.Y);
            minX = tiles.Min(t => t.X);
            maxX = tiles.Max(t => t.X);
        }

        public void TestCase()
        {
            ReadData();
            Day17();
        }
    }
}
