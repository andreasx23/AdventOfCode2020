using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day10
{
    public class Day10Part1
    {
        class Tile
        {
            public int X;
            public int Y;
            public int VelocityX;
            public int VelocityY;
        }

        private readonly List<Tile> tiles = new List<Tile>();

        private void Day10()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int minX = tiles.Min(t => t.X);
            int minY = tiles.Min(t => t.Y);
            int maxX = tiles.Max(t => t.X);
            int maxY = tiles.Max(t => t.Y);
            bool isFinished = false;
            int index = 0;
            while (!isFinished)
            {
                List<Tile> temp = new List<Tile>(tiles);
                for (int i = 0; i < tiles.Count; i++)
                {
                    var current = tiles[i];
                    current.X += current.VelocityX;
                    current.Y += current.VelocityY;
                    tiles[i] = current;
                }

                int newMinX = tiles.Min(t => t.X);
                int newMinY = tiles.Min(t => t.Y);
                int newMaxX = tiles.Max(t => t.X);
                int newMaxY = tiles.Max(t => t.Y);
                if ((newMaxX - newMinX) > (maxX - minX) || (newMaxY - newMinY) > (maxY - minY))
                {
                    Console.WriteLine("Total rounds: " + index);
                    for (int i = minX - 25; i <= maxX + 25; i++)
                    {
                        for (int j = minY - 25; j <= maxY + 25; j++)
                        {
                            Console.Write(temp.Any(t => t.X == i && t.Y == j) ? "#" : ".");
                        }
                        Console.WriteLine();
                    }
                    isFinished = true;
                }

                minX = newMinX;
                minY = newMinY;
                maxX = newMaxX;
                maxY = newMaxY;
                index++;
            }

            int ans = 0;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day10\sample.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Replace("position=<", "").Replace("> velocity=<", "#").Replace(">", "").Split('#').ToList());

            foreach (var s in lines)
            {
                var XY = s[0].Split(',').Select(int.Parse).ToList();
                var VelocityXY = s[1].Split(',').Select(int.Parse).ToList();
                tiles.Add(new Tile()
                {
                    X = XY[1],
                    Y = XY[0],
                    VelocityX = VelocityXY[1],
                    VelocityY = VelocityXY[0]
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day10();
        }
    }
}
