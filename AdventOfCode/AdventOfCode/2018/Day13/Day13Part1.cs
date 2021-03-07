using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day13
{
    public class Day13Part1
    {
        class Minecart
        {
            public int X;
            public int Y;
            public char Value;
            public char Direction;
            private int IntersectionState;
            public bool HitCornerOrIntersection;

            public Minecart()
            {
                IntersectionState = 1;
            }

            public int GetIntersectionState()
            {
                if (IntersectionState == 1) //Left
                {
                    IntersectionState = 2;
                    return 1;
                }
                else if (IntersectionState == 2) //Straight
                {
                    IntersectionState = 3;
                    return 2;
                }
                else //Right
                {
                    IntersectionState = 1;
                    return 3;
                }
            }
        }

        private char[][] grid = null;
        private int H = 0, W = 0;

        private readonly List<Minecart> minecarts = new List<Minecart>();
        private List<char> directions = new List<char>() { 'v', '<', '>', '^' };
        private List<char> tracks = new List<char>() { '+', '|', '-', '/', '\\' };

        //https://adventofcode.com/2018/day/13
        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool isFinished = false;
            (int X, int Y) ans = (-1, -1);
            while (!isFinished)
            {
                foreach (var minecart in minecarts)
                {
                    char currPos = 'X';
                    
                    switch (minecart.Direction)
                    {
                        case '<':
                            {
                                if (minecart.Y == 0)
                                {


                                    currPos = grid[minecart.X - 1][0];
                                }

                                if (minecart.X == 0 && minecart.Y == 0)
                                {
                                    currPos = grid[1][0];
                                }
                                else if (minecart.X == H - 1 && minecart.Y == 0)
                                {
                                    currPos = grid[H - 1][0];
                                }
                                else
                                {
                                    currPos = grid[minecart.X][minecart.Y - 1];
                                }

                                if (minecart.HitCornerOrIntersection)
                                {
                                    if (currPos == '-')
                                    {
                                        minecart.Y += -1;
                                        minecart.HitCornerOrIntersection = false;
                                    }
                                    else if (currPos == '|')
                                    {
                                        //Not valid direction
                                    }
                                }
                                else
                                {
                                    if (currPos == '/')
                                    {
                                        minecart.Direction = 'v';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                    else if (currPos == '\\')
                                    {
                                        minecart.Direction = '^';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                }
                            }
                            break;
                        case '>':
                            {

                                if (minecart.HitCornerOrIntersection)
                                {
                                    if (currPos == '-')
                                    {
                                        minecart.Y += -1;
                                        minecart.HitCornerOrIntersection = false;
                                    }
                                    else if (currPos == '|')
                                    {
                                        //Not valid direction
                                    }
                                }
                                else
                                {
                                    if (currPos == '/')
                                    {
                                        minecart.Direction = '^';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                    else if (currPos == '\\')
                                    {
                                        minecart.Direction = 'v';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                }
                            }
                            break;
                        case '^':
                            {
                                if (minecart.HitCornerOrIntersection)
                                {
                                    if (currPos == '-')
                                    {
                                        //Not valid direction
                                    }
                                    else if (currPos == '|')
                                    {
                                        minecart.X += -1;
                                        minecart.HitCornerOrIntersection = false;
                                    }
                                }
                                else
                                {
                                    if (currPos == '/')
                                    {
                                        minecart.Direction = '>';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                    else if (currPos == '\\')
                                    {
                                        minecart.Direction = '<';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                }                                
                            }
                            break;
                        case 'v':
                            {
                                if (minecart.HitCornerOrIntersection)
                                {
                                    if (currPos == '-')
                                    {
                                        //Not valid direction
                                    }
                                    else if (currPos == '|')
                                    {
                                        minecart.X += 1;
                                        minecart.HitCornerOrIntersection = false;
                                    }
                                }
                                else
                                {
                                    if (currPos == '/')
                                    {
                                        minecart.Direction = '<';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                    else if (currPos == '\\')
                                    {
                                        minecart.Direction = '>';
                                        minecart.HitCornerOrIntersection = true;
                                    }
                                }
                            }
                            break;
                        default:
                            throw new Exception();
                    }
                }

                foreach (var minecart in minecarts)
                {
                    if (minecarts.Any(m => m.Value != minecart.Value && m.X == minecart.X && m.Y == minecart.Y))
                    {
                        ans = (minecart.X, minecart.Y);
                        isFinished = true;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char nextStep(char[][] grid, Minecart minecart)
        {
            List<(int X, int Y)> dirs = new List<(int X, int Y)>()
            {
                (-1, 0), //Up
                (1, 0),  //Down
                (0, -1), //Left
                (0, 1),  //Right
            };

            char step = '#';
            foreach (var d in dirs)
            {
                int tempX = d.X + minecart.X;
                int tempY = d.Y + minecart.Y;

                if (tempX < 0 || tempX >= grid.Length || tempY < 0 || tempY >= grid[0].Length)
                {
                    continue;
                }

                switch (minecart.Direction)
                {
                    case '<':

                        break;
                    case '>':

                        break;
                    case '^':
                        break;
                    case 'v':
                        break;
                    default:
                        throw new Exception();
                }
            }

            return step;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day13\sample.txt";
            var lines = File.ReadAllLines(path);

            H = lines.Length;
            grid = new char[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = lines[i].ToCharArray();
                W = Math.Max(W, lines[i].Length);
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (directions.Contains(grid[i][j]))
                    {
                        minecarts.Add(new Minecart()
                        {
                            X = i,
                            Y = j,
                            Direction = directions.Find(s => s == grid[i][j]),
                            Value = Convert.ToChar(i + 'A')
                        });
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day13();
        }
    }
}
