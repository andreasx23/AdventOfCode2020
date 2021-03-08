﻿using System;
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
            public char ID;
            public char Direction;
            private int IntersectionState;

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

        //https://adventofcode.com/2018/day/13
        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int turns = 0;
            bool isFinished = false;
            (int X, int Y) ans = (0, 0);
            while (!isFinished)
            {
                turns++;
                foreach (var minecart in minecarts)
                {
                    char currPos = grid[minecart.X][minecart.Y];

                    //Console.WriteLine(minecart.ID + " " + currPos + " " + minecart.Direction);
                    switch (minecart.Direction)
                    {
                        case '<':
                            {
                                if (currPos == '-')
                                {
                                    minecart.Y += -1;
                                }
                                else if (currPos == '|')
                                {
                                    Console.WriteLine("Not valid");
                                    //Not valid direction
                                }
                                else if (currPos == '/')
                                {
                                    minecart.Direction = 'v';
                                    minecart.X += 1;
                                }
                                else if (currPos == '\\')
                                {
                                    minecart.Direction = '^';
                                    minecart.X += -1;
                                }
                                else if (currPos == '+')
                                {
                                    var state = minecart.GetIntersectionState();

                                    if (state == 1)
                                    {
                                        minecart.Direction = 'v';
                                        minecart.X += 1;
                                    }
                                    else if (state == 2)
                                    {
                                        minecart.Direction = '<';
                                        minecart.Y += -1;
                                    }
                                    else
                                    {
                                        minecart.Direction = '^';
                                        minecart.X += -1;
                                    }
                                }
                            }
                            break;
                        case '>':
                            {
                                if (currPos == '-')
                                {
                                    minecart.Y += 1;
                                }
                                else if (currPos == '|')
                                {
                                    Console.WriteLine("Not valid");
                                    //Not valid direction
                                }
                                else if (currPos == '/')
                                {
                                    minecart.Direction = '^';
                                    minecart.X += -1;
                                }
                                else if (currPos == '\\')
                                {
                                    minecart.Direction = 'v';
                                    minecart.X += 1;
                                }
                                else if (currPos == '+')
                                {
                                    var state = minecart.GetIntersectionState();

                                    if (state == 1)
                                    {
                                        minecart.Direction = '^';
                                        minecart.X += -1;
                                    }
                                    else if (state == 2)
                                    {
                                        minecart.Direction = '>';
                                        minecart.Y += 1;
                                    }
                                    else
                                    {
                                        minecart.Direction = 'v';
                                        minecart.X += 1;
                                    }
                                }
                            }
                            break;
                        case '^':
                            {
                                if (currPos == '-')
                                {
                                    Console.WriteLine("Not valid");
                                    //Not valid direction
                                }
                                else if (currPos == '|')
                                {
                                    minecart.X += -1;
                                }
                                else if (currPos == '/')
                                {
                                    minecart.Direction = '>';
                                    minecart.Y += 1;
                                }
                                else if (currPos == '\\')
                                {
                                    minecart.Direction = '<';
                                    minecart.Y += -1;
                                }
                                else if (currPos == '+')
                                {
                                    var state = minecart.GetIntersectionState();

                                    if (state == 1)
                                    {
                                        minecart.Direction = '<';
                                        minecart.Y += -1;
                                    }
                                    else if (state == 2)
                                    {
                                        minecart.Direction = '^';
                                        minecart.X += -1;
                                    }
                                    else
                                    {
                                        minecart.Direction = '>';
                                        minecart.Y += 1;
                                    }
                                }
                            }
                            break;
                        case 'v':
                            {
                                if (currPos == '-')
                                {
                                    Console.WriteLine("Not valid");
                                    //Not valid direction
                                }
                                else if (currPos == '|')
                                {
                                    minecart.X += 1;
                                }
                                else if (currPos == '/')
                                {
                                    minecart.Direction = '<';
                                    minecart.Y += -1;
                                }
                                else if (currPos == '\\')
                                {
                                    minecart.Direction = '>';
                                    minecart.Y += 1;
                                }
                                else if (currPos == '+')
                                {
                                    var state = minecart.GetIntersectionState();

                                    if (state == 1)
                                    {
                                        minecart.Direction = '>';
                                        minecart.Y += 1;
                                    }
                                    else if (state == 2)
                                    {
                                        minecart.Direction = 'v';
                                        minecart.X += 1;
                                    }
                                    else
                                    {
                                        minecart.Direction = '<';
                                        minecart.Y += -1;
                                    }
                                }
                            }
                            break;
                        default:
                            throw new Exception();
                    }

                    if (minecarts.Any(m => m.ID != minecart.ID && m.X == minecart.X && m.Y == minecart.Y))
                    {
                        //Console.WriteLine(turns);
                        //grid[minecart.X][minecart.Y] = '#';
                        //Print();

                        ans = (minecart.X, minecart.Y);
                        isFinished = true;
                        break;
                    }
                }

                //Print();
                //Console.WriteLine();
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans.Y},{ans.X} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print()
        {
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    var cart = minecarts.FirstOrDefault(m => i == m.X && j == m.Y);
                    if (cart != null)
                    {
                        Console.Write(cart.ID);
                    }
                    else
                    {
                        Console.Write(grid[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2018\Day13\input.txt";
            var lines = File.ReadAllLines(path);

            List<char> directions = new List<char>() { 'v', '<', '>', '^' };

            H = lines.Length;
            grid = new char[H][];
            int index = 0;
            for (int i = 0; i < H; i++)
            {
                grid[i] = lines[i].ToCharArray();
                W = Math.Max(W, lines[i].Length);
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (directions.Contains(grid[i][j]))
                    {
                        var dir = directions.Find(s => s == grid[i][j]);
                        minecarts.Add(new Minecart()
                        {
                            X = i,
                            Y = j,
                            Direction = dir,
                            ID = Convert.ToChar(index + 'A')
                        });

                        index++;

                        if (dir == '<' || dir == '>')
                        {
                            grid[i][j] = '-';
                        }
                        else
                        {
                            grid[i][j] = '|';
                        }
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
