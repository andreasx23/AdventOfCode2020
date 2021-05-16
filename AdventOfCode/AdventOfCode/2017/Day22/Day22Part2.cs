using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day22
{
    public class Day22Part2
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
            for (int i = 0; i < 10000000; i++)
            {
                if (!map.ContainsKey((x, y))) map.Add((x, y), new Node());
                Node node = map[(x, y)];

                switch (dir)
                {
                    case Direction.LEFT:
                        {
                            switch (node.State)
                            {
                                case State.CLEAN:
                                    dir = Direction.DOWN;
                                    x++;
                                    break;
                                case State.WEAKENED:
                                    y--;
                                    break;
                                case State.INFECTED:
                                    dir = Direction.UP;
                                    x--;
                                    break;
                                case State.FLAGGED:
                                    dir = Direction.RIGHT;
                                    y++;
                                    break;
                                default:
                                    throw new Exception();
                            }
                        }
                        break;
                    case Direction.RIGHT:
                        {
                            switch (node.State)
                            {
                                case State.CLEAN:
                                    dir = Direction.UP;
                                    x--;
                                    break;
                                case State.WEAKENED:
                                    y++;
                                    break;
                                case State.INFECTED:
                                    dir = Direction.DOWN;
                                    x++;
                                    break;
                                case State.FLAGGED:
                                    dir = Direction.LEFT;
                                    y--;
                                    break;
                                default:
                                    throw new Exception();
                            }
                        }
                        break;
                    case Direction.UP:
                        {
                            switch (node.State)
                            {
                                case State.CLEAN:
                                    dir = Direction.LEFT;
                                    y--;
                                    break;
                                case State.WEAKENED:
                                    x--;
                                    break;
                                case State.INFECTED:
                                    dir = Direction.RIGHT;
                                    y++;
                                    break;
                                case State.FLAGGED:
                                    dir = Direction.DOWN;
                                    x++;
                                    break;
                                default:
                                    throw new Exception();
                            }
                        }
                        break;
                    case Direction.DOWN:
                        {
                            switch (node.State)
                            {
                                case State.CLEAN:
                                    dir = Direction.RIGHT;
                                    y++;
                                    break;
                                case State.WEAKENED:
                                    x++;
                                    break;
                                case State.INFECTED:
                                    dir = Direction.LEFT;
                                    y--;
                                    break;
                                case State.FLAGGED:
                                    dir = Direction.UP;
                                    x--;
                                    break;
                                default:
                                    throw new Exception();
                            }
                        }
                        break;
                    default:
                        throw new Exception();
                }

                switch (node.State)
                {
                    case State.CLEAN:
                        node.State = State.WEAKENED;
                        break;
                    case State.WEAKENED:
                        node.State = State.INFECTED;
                        ans++;
                        break;
                    case State.INFECTED:
                        node.State = State.FLAGGED;
                        break;
                    case State.FLAGGED:
                        node.State = State.CLEAN;
                        break;
                    default:
                        break;
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
                        State = (grid[i][j] == '#') ? State.INFECTED : State.CLEAN
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
