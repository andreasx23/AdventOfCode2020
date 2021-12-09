using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day09
{
    public class Day9Part1
    {
        private int[][] _grid;

        private void Day9()
        {
            Stopwatch watch = Stopwatch.StartNew();

            List<(int x, int y)> dirs = new List<(int x, int y)>()
            {
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1)
            };

            int h = _grid.Length, w = _grid.First().Length, ans = 0;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    int current = _grid[i][j];
                    List<int> neighbours = new List<int>();
                    foreach (var (x, y) in dirs)
                    {
                        int dx = i + x, dy = j + y;
                        if (dx < 0 || dx >= h || dy < 0 || dy >= w) continue;
                        neighbours.Add(_grid[dx][dy]);
                    }
                    if (neighbours.All(n => n > current))
                        ans += current + 1;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day09\input.txt";
            _grid = File.ReadAllLines(path).Select(row => row.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
