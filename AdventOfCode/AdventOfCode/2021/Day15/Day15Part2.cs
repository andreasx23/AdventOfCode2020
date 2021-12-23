using AlgoKit.Collections.Heaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day15
{
    public class Day15Part2
    {
        private int[][] _Initialgrid;
        private int[][] _grid;

        private void Day15()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int h = _grid.Length, w = _grid.First().Length;
            bool[,] isVisisted = new bool[h, w];
            Comparer<int> comparer = Comparer<int>.Default;
            PairingHeap<int, (int x, int y)> queue = new PairingHeap<int, (int x, int y)>(comparer);
            foreach (var (x, y) in Neighbours(0, 0))
            {
                queue.Add(_grid[x][y], (x, y));
                isVisisted[x, y] = true;
            }

            int ans = 0;
            while (queue.Any())
            {
                PairingHeapNode<int, (int x, int y)> current = queue.Pop();
                
                if (current.Value.x == h - 1 && current.Value.y == w - 1)
                {
                    ans = current.Key;
                    break;
                }

                foreach (var (x, y) in Neighbours(current.Value.x, current.Value.y))
                {
                    if (!isVisisted[x, y])
                    {
                        isVisisted[x, y] = true;
                        queue.Add(current.Key + _grid[x][y], (x, y));
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private List<(int x, int y)> Neighbours(int x, int y)
        {
            List<(int x, int y)> dirs = new List<(int x, int y)>()
            {
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1),
            };
            List<(int x, int y)> valids = new List<(int x, int y)>();
            foreach (var item in dirs)
            {
                int dx = x + item.x, dy = y + item.y;
                if (dx < 0 || dx >= _grid.Length || dy < 0 || dy >= _grid.First().Length) continue;
                valids.Add((dx, dy));
            }
            return valids;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\Advent_of_Code\2021\Day15\input.txt";
            _Initialgrid = File.ReadAllLines(path).Select(row => row.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            _grid = new int[_Initialgrid.Length * 5][];
            for (int i = 0; i < _grid.Length; i++)
                _grid[i] = new int[_Initialgrid.First().Length * 5];

            bool isFirst = true;
            int row = 0;
            for (int runs = 0; runs < 5; runs++)
            {
                for (int i = 0; i < _Initialgrid.Length; i++)
                {
                    for (int j = 0; j < _Initialgrid[i].Length; j++)
                    {
                        if (isFirst)
                            _grid[i][j] = _Initialgrid[i][j];
                        else
                        {
                            _Initialgrid[i][j] = _Initialgrid[i][j] + 1;
                            if (_Initialgrid[i][j] >= 10) _Initialgrid[i][j] = 1;
                            _grid[i][j + row] = _Initialgrid[i][j];
                        }
                    }
                }
                isFirst = false;
                row += _Initialgrid.First().Length;
            }

            for (int i = _Initialgrid.Length; i < _grid.Length; i++)
            {
                for (int j = 0; j < _grid[i].Length; j++)
                {
                    int val = _grid[i - _Initialgrid.Length][j] + 1;
                    if (val >= 10) val = 1;
                    _grid[i][j] = val;
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day15();
        }
    }
}
