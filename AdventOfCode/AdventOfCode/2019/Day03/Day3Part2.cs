using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day03
{
    public class Day3Part2
    {
        private readonly List<List<string>> wires = new List<List<string>>();
        private int height, width;
        private char[,] grid;
        private bool[,] isVisited;

        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            grid = new char[height, width];
            isVisited = new bool[height, width];

            //for (int initI = 0; initI < height; initI++)
            //{
            //    for (int initJ = 0; initJ < width; initJ++)
            //    {
            //        grid[initI, initJ] = '.';
            //    }
            //}

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
                            case 'R': //Right
                                j++;
                                grid[i, j] = '-';
                                break;
                            case 'L': //Left
                                j--;
                                grid[i, j] = '-';
                                break;
                            case 'U': //Up
                                i--;
                                grid[i, j] = '|';
                                break;
                            case 'D': //Down
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

            //Print(grid);

            int ans = int.MaxValue;
            foreach (var tuple in postions)
            {
                char[,] clone = (char[,])grid.Clone();
                int steps = DFS(startPos.i, startPos.j, 0, (tuple.i, tuple.j), clone);
                ans = Math.Min(ans, steps);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private int DFS(int i, int j, int steps, (int i, int j) target, char[,] grid)
        {
            if (!isVisited[i, j] || grid[i, j] == '/')
            {
                return 0;
            }

            if (i == target.i && j == target.j)
            {
                //Print(grid);
                return steps;
            }

            grid[i, j] = '/';

            return 0 + DFS(i + 1, j, steps + 1, target, grid) +
                       DFS(i - 1, j, steps + 1, target, grid) +
                       DFS(i, j + 1, steps + 1, target, grid) +
                       DFS(i, j - 1, steps + 1, target, grid);
        }

        private bool IsX(int i, int j)
        {
            return isVisited[i, j] &&
                   isVisited[i - 1, j] &&
                   isVisited[i + 1, j] &&
                   isVisited[i, j - 1] &&
                   isVisited[i, j + 1];
        }

        private void Print(char[,] grid)
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

        private void Steal()
        {
            var line1 = DrawLine(wires[0]);
            var line2 = DrawLine(wires[1]);

            var intersections = line1.Intersect(line2).ToList();

            var dist1 = line1.IndexOf(intersections.First()) + 1;
            var dist2 = line2.IndexOf(intersections.First()) + 1;

            Console.WriteLine(dist1 + " " + dist2);

            var totalStepsToFirstIntersection = dist1 + dist2;

            Console.WriteLine("Answer: " + totalStepsToFirstIntersection);
        }

        public static List<Point> DrawLine(List<string> path)
        {
            var lastPoint = new Point(0, 0);
            var line = new List<Point>();

            foreach (string instruction in path)
            {
                Evaluate(lastPoint, instruction, line);
                lastPoint = line.Last();
            }

            return line;
        }

        public static void Evaluate(Point point, string action, List<Point> line)
        {
            var operation = action[0].ToString();
            var distance = int.Parse(action.Substring(1));

            switch (operation)
            {
                case "U":
                    for (int i = 1; i <= distance; i++)
                    {
                        line.Add(new Point(point.X, point.Y + i));
                    }
                    break;
                case "L":
                    for (int i = 1; i <= distance; i++)
                    {
                        line.Add(new Point(point.X - i, point.Y));
                    }
                    break;
                case "R":
                    for (int i = 1; i <= distance; i++)
                    {
                        line.Add(new Point(point.X + i, point.Y));
                    }
                    break;
                case "D":
                    for (int i = 1; i <= distance; i++)
                    {
                        line.Add(new Point(point.X, point.Y - i));
                    }
                    break;
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day03\input.txt";
            var input = File.ReadAllLines(path);

            foreach (var s in input)
            {
                var instructions = s.Split(',');

                List<string> toAdd = new List<string>();
                foreach (var instruction in instructions)
                {
                    char direction = instruction[0];
                    int distance = int.Parse(instruction.Substring(1));

                    if (direction == 'R' || direction == 'L')
                    {
                        width = Math.Max(width, distance);
                    }
                    else
                    {
                        height = Math.Max(height, distance);
                    }
                    toAdd.Add(instruction);
                }
                wires.Add(toAdd);
            }

            height += 30003;
            width += 30003;

            //int divider = 5;
            //height /= divider;
            //width /= divider;
        }

        public void TestCase()
        {
            ReadData();
            //Day3();
            Steal();
        }
    }
}
