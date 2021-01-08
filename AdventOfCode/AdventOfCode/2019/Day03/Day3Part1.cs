using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day03
{
    public class Day3Part1
    {
        private readonly List<List<string>> wires = new List<List<string>>();
        private int height, width;
        private char[,] grid;
        private bool[,] isVisited;
        
        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            height /= 5;
            width /= 5;

            grid = new char[height, width];
            isVisited = new bool[height, width];

            for (int initI = 0; initI < height; initI++)
            {
                for (int initJ = 0; initJ < width; initJ++)
                {
                    grid[initI, initJ] = '.';
                }
            }

            (int i, int j) startPos = (height / 2, width / 2);
            foreach (var wire in wires)
            {
                int i = startPos.i, j = startPos.j, n = wire.Count;

                grid[i, j] = 'O';
                isVisited[i, j] = true;

                for (int index = 0; index < n; index++)
                {
                    char dir = wire[index][0];
                    int steps = int.Parse(wire[index].Substring(1));

                    for (int k = 0; k < steps; k++)
                    {
                        switch (dir)
                        {
                            case 'R':
                                j++;
                                grid[i, j] = '-';
                                break;
                            case 'L':
                                j--;
                                grid[i, j] = '-';
                                break;
                            case 'U':
                                i--;
                                grid[i, j] = '|';
                                break;
                            case 'D':
                                i++;
                                grid[i, j] = '|';
                                break;
                        }

                        isVisited[i, j] = true;
                    }

                    grid[i, j] = (IsX(i, j)) ? 'X' : '+';
                }
            }

            List<(int i, int j)> postions = new List<(int, int)>();
            for (int initI = 0; initI < height; initI++)
            {
                for (int initJ = 0; initJ < width; initJ++)
                {
                    if (IsX(initI, initJ))
                    {
                        grid[initI, initJ] = 'X';
                        postions.Add((initI, initJ));
                    }
                }
            }

            Print();

            int ans = int.MaxValue;
            foreach (var tuple in postions)
            {
                int i = Math.Abs(startPos.i - tuple.i);
                int j = Math.Abs(startPos.j - tuple.j);
                int sum = i + j;
                ans = Math.Min(ans, sum);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsX(int i, int j)
        {
            return isVisited[i, j] && 
                   isVisited[i - 1, j] && 
                   isVisited[i + 1, j] && 
                   isVisited[i, j - 1] && 
                   isVisited[i, j + 1];
        }

        private void Print()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day03\input.txt";
            var input = File.ReadAllLines(path);

            foreach (var s in input)
            {
                var _moves = s.Split(',');

                List<string> toAdd = new List<string>();
                foreach (var move in _moves)
                {
                    char c = move[0];
                    if (c == 'R' || c == 'L')
                    {
                        //width = Math.Max(width, int.Parse(move.Substring(1)));
                        width += int.Parse(move.Substring(1));
                    }
                    else
                    {
                        //height = Math.Max(height, int.Parse(move.Substring(1)));
                        height += int.Parse(move.Substring(1));
                    }
                    toAdd.Add(move);
                }
                wires.Add(toAdd);
            }

            height += 3;
            width += 3;
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
