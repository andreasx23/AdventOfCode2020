using AdventOfCode._2016.Day05;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day17
{
    public class Day17Part2
    {
        private static readonly bool isSample = false;
        private readonly string input = (isSample) ? "ulqzkmiv" : "udskfozm";

        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int width = 4, height = 4;
            char[][] grid = new char[height][];
            for (int i = 0; i < height; i++)
            {
                grid[i] = new char[width];
                for (int j = 0; j < width; j++)
                {
                    grid[i][j] = '.';
                }
            }

            Tile start = new Tile() { X = 0, Y = 0 };
            Tile target = new Tile() { X = 3, Y = 3 };

            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(start);

            int ans = 0;
            while (queue.Any())
            {
                var current = queue.Dequeue();
                var hash = GetHash(input + current.Path);

                if (current.X == target.X && current.Y == target.Y)
                {
                    ans = Math.Max(ans, current.Path.Length);
                    continue;
                }

                var walkable = Walkable(grid, current, hash);
                foreach (var next in walkable)
                {
                    next.Path = current.Path + next.Dir;
                    queue.Enqueue(next);
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<Tile> Walkable(char[][] grid, Tile current, string hash)
        {
            List<Tile> neighbours = new List<Tile>()
            {
                new Tile() { X = -1, Y = 0, Dir = 'U' },
                new Tile() { X = 1, Y = 0, Dir = 'D' },
                new Tile() { X = 0, Y = -1, Dir = 'L' },
                new Tile() { X = 0, Y = 1, Dir = 'R' }
            };

            List<Tile> valids = new List<Tile>();
            for (int i = 0; i < 4; i++)
            {
                Tile next = neighbours[i];
                next.X += current.X;
                next.Y += current.Y;

                if (next.X < 0 || next.X >= grid.Length || next.Y < 0 || next.Y >= grid[0].Length || !IsDoorOpen(hash[i])) continue;

                valids.Add(next);
            }
            return valids;
        }

        private bool IsDoorOpen(char path)
        {
            List<char> valids = new List<char>() { 'b', 'c', 'd', 'e', 'f' };
            return valids.Contains(path);
        }

        private string GetHash(string path)
        {
            return new string(MD5Hash.CreateMD5(path).Take(4).ToArray()).ToLower();
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
            Day17();
        }
    }
}
