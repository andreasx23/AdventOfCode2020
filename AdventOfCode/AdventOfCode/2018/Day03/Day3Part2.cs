using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day03
{
    public class Day3Part2
    {
        class Area
        {
            public int id;
            public int leftEdge;
            public int topEdge;
            public int width;
            public int height;
            public bool isIntact;

            override
            public string ToString()
            {
                return $"{id} {leftEdge} {topEdge} {width} {height}";
            }
        }

        private readonly List<Area> areas = new List<Area>();

        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int row = 1000;
            int column = 1000;

            var grid = GenerateGrid(row, column);
            foreach (var area in areas)
            {
                for (int i = 0; i < area.width; i++)
                {
                    for (int k = 0; k < area.height; k++)
                    {
                        if (grid[area.leftEdge + i][area.topEdge + k] == '.')
                        {
                            grid[area.leftEdge + i][area.topEdge + k] = 'O';
                        }
                        else if (grid[area.leftEdge + i][area.topEdge + k] == 'O')
                        {
                            grid[area.leftEdge + i][area.topEdge + k] = 'X';
                        }
                    }
                }
            }

            //Print(grid);

            foreach (var area in areas)
            {
                bool isIntact = true;
                for (int i = 0; i < area.width; i++)
                {
                    for (int k = 0; k < area.height; k++)
                    {
                        if (grid[area.leftEdge + i][area.topEdge + k] == 'X')
                        {
                            isIntact = false;
                        }
                    }
                }
                area.isIntact = isIntact;
            }

            int ans = areas.First(a => a.isIntact).id;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char[][] GenerateGrid(int row, int column)
        {
            char[][] grid = new char[row][];
            for (int i = 0; i < row; i++)
            {
                grid[i] = new char[column];

                for (int j = 0; j < column; j++)
                {
                    grid[i][j] = '.';
                }
            }

            return grid;
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join(" ", item));
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day03\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(' ');

                var id = int.Parse(split[0].Substring(1));
                var edges = split[2].Split(',');
                var leftEdge = int.Parse(edges[0]);
                var topEdge = int.Parse(edges[1].Substring(0, edges[1].Length - 1));
                var grid = split[3].Split('x');
                var width = int.Parse(grid[0]);
                var height = int.Parse(grid[1]);

                areas.Add(new Area()
                {
                    id = id,
                    leftEdge = leftEdge,
                    topEdge = topEdge,
                    width = width,
                    height = height
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
