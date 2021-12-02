using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day18
{
    public class Day18Part1
    {
        private char[][] _grid;
        private int _h, _w;

        enum Types
        {
            OPEN_GROUND = '.',
            TREE = '|',
            LUMBERYARD = '#'
        }

        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Console.WriteLine("Initial:");
            //Print(_grid);

            int minutes = 10;
            for (int i = 0; i < minutes; i++)
            {
                char[][] grid = NewGrid();

                for (int j = 0; j < _h; j++)
                {
                    for (int k = 0; k < _w; k++)
                    {
                        Types current = (Types)_grid[j][k];
                        bool shouldTransform = ShouldTransform(current, j, k);
                        switch (current)
                        {
                            case Types.OPEN_GROUND:
                                {
                                    if (shouldTransform)
                                        grid[j][k] = (char)Types.TREE;
                                    else
                                        grid[j][k] = (char)current;
                                }
                                break;
                            case Types.TREE:
                                {
                                    if (shouldTransform) 
                                        grid[j][k] = (char)Types.LUMBERYARD;
                                    else
                                        grid[j][k] = (char)current;
                                }
                                break;
                            case Types.LUMBERYARD:
                                {
                                    if (!shouldTransform) 
                                        grid[j][k] = (char)Types.OPEN_GROUND;
                                    else
                                        grid[j][k] = (char)current;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

                //Print(grid);

                _grid = grid;
            }

            //Print(_grid);

            int ans = _grid.Sum(array => array.Count(type => (Types)type == Types.TREE)) * _grid.Sum(array => array.Count(type => (Types)type == Types.LUMBERYARD));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
            Console.WriteLine();
        }

        private char[][] NewGrid()
        {
            char[][] grid = new char[_h][];
            for (int i = 0; i < _h; i++)
            {
                grid[i] = new char[_w];
            }
            return grid;
        }

        private bool ShouldTransform(Types current, int x, int y)
        {
            List<Types> neighbours = Neighbours(x, y);
            switch (current)
            {
                case Types.OPEN_GROUND:
                    {
                        var trees = neighbours.Count(n => n == Types.TREE);
                        return trees >= 3;
                    }
                case Types.TREE:
                    {
                        var lumberyards = neighbours.Count(n => n == Types.LUMBERYARD);
                        return lumberyards >= 3;
                    }
                case Types.LUMBERYARD:
                    {
                        var lumberyards = neighbours.Count(n => n == Types.LUMBERYARD);
                        var trees = neighbours.Count(n => n == Types.TREE);
                        return lumberyards >= 1 && trees >= 1;
                    }
                default:
                    return false;
            }
        }

        private List<Types> Neighbours(int x, int y)
        {
            List<(int x, int y)> dirs = new List<(int x, int y)>()
            {
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1),
                (-1, -1),
                (-1, 1),
                (1, -1),
                (1, 1)
            };

            List<Types> neighbours = new List<Types>();
            foreach ((int x, int y) item in dirs)
            {
                int dx = item.x + x;
                int dy = item.y + y;

                if (dx < 0 || dx >= _h || dy < 0 || dy >= _w) continue;

                Types neighbour = (Types)_grid[dx][dy];
                neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2018\Day18\input.txt";
            var lines = File.ReadAllLines(path);
            _h = lines.Length;
            _w = lines.First().Length;
            _grid = new char[_h][];
            for (int i = 0; i < _h; i++)
            {
                _grid[i] = lines[i].ToCharArray();
            }
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
