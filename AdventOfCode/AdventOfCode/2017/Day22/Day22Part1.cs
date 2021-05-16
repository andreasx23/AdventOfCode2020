using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day22
{
    public class Day22Part1
    {
        private char[][] grid = null;
        private int H = 0, W = 0;
        private readonly Dictionary<(int x, int y), Node> map = new Dictionary<(int x, int y), Node>();

        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            int x = (H % 2 == 0) ? H / 2 - 1 : H / 2;
            int y = (W % 2 == 0) ? W / 2 - 1 : W / 2;
            Direction dir = Direction.UP;
            for (int i = 0; i < 10000; i++)
            {
                if (!map.ContainsKey((x, y))) map.Add((x, y), new Node());
                Node node = map[(x, y)];

                switch (dir)
                {
                    case Direction.LEFT:
                        {
                            if (node.IsInfected)
                            {
                                dir = Direction.UP;
                                x--;
                            }
                            else
                            {
                                dir = Direction.DOWN;
                                x++;
                            }
                        }
                        break;
                    case Direction.RIGHT:
                        {
                            if (node.IsInfected)
                            {
                                dir = Direction.DOWN;
                                x++;
                            }
                            else
                            {
                                dir = Direction.UP;
                                x--;
                            }
                        }
                        break;
                    case Direction.UP:
                        {
                            if (node.IsInfected)
                            {
                                dir = Direction.RIGHT;
                                y++;
                            }
                            else
                            {
                                dir = Direction.LEFT;
                                y--;
                            }
                        }
                        break;
                    case Direction.DOWN:
                        {
                            if (node.IsInfected)
                            {
                                dir = Direction.LEFT;
                                y--;
                            }
                            else
                            {
                                dir = Direction.RIGHT;
                                y++;
                            }
                        }
                        break;
                    default:
                        throw new Exception();
                }

                if (!node.IsInfected)
                {
                    ans++;
                    node.IsInfected = true;
                }
                else
                {
                    node.IsInfected = false;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2017\Day22\input.txt";
            var lines = File.ReadAllLines(path);
            H = lines.Length;
            W = lines.First().Length;
            grid = new char[H][];

            for (int i = 0; i < H; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    map.Add((i, j), new Node()
                    {
                        IsInfected = (grid[i][j] == '#')
                    });
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}
