using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day05
{
    public class Day5Part1
    {
        private readonly List<(int x1, int y1, int x2, int y2)> _lines = new List<(int x1, int y1, int x2, int y2)>();
        private int[][] _grid;

        private void Day5()
        {
            Stopwatch watch = Stopwatch.StartNew();

            foreach (var (x1, y1, x2, y2) in _lines)
            {
                Console.WriteLine($"{x1} {y1} -> {x2} {y2}");
                if (x1 == x2 || y1 == y2)
                {
                    for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
                        for (int j = Math.Min(y1, y2); j <= Math.Max(y1, y2); j++)
                            _grid[i][j]++;
                }
            }

            int ans = _grid.Sum(row => row.Count(n => n > 1));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day05\input.txt";
            var input = File.ReadAllLines(path).Select(s => s.Replace(" -> ", " ")).ToList();
            int height = -1;
            int width = -1;
            foreach (var item in input)
            {
                var split = item.Split(' ').Select(s => s.Split(',').Select(int.Parse).ToList()).ToList();
                var x1 = split[0][0];
                var y1 = split[0][1];
                var x2 = split[1][0];
                var y2 = split[1][1];
                _lines.Add((y1, x1, y2, x2));
                height = Math.Max(height, Math.Max(y1, y2));
                width = Math.Max(width, Math.Max(x1, x2));
            }

            Console.WriteLine(height + " " + width);

            _grid = new int[height + 1][];
            for (int i = 0; i < _grid.Length; i++)
                _grid[i] = new int[width + 1];
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
