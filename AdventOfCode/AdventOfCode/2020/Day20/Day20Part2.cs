using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020.Day20
{
    public class Day20Part2
    {
        private readonly Dictionary<int, char[][]> grids = new Dictionary<int, char[][]>();
        private readonly Dictionary<char[][], long> ids = new Dictionary<char[][], long>();
        private readonly List<string> monster = new List<string>()
        {
            "                  # ",
            "#    ##    ##    ###",
            " #  #  #  #  #  #   "
        };

        private void Day20()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int size = 12;
            var image = GenerateImage(size); //3 - 12

            var grid = GenerateGrid(size * 8); //24 - 96
            int index = 0;
            for (int i = 0; i < image.Count; i++)
            {
                List<char[][]> arrays = new List<char[][]>();
                for (int j = 0; j < image[i].Count; j++)
                {
                    arrays.Add(image[i][j]);

                }

                List<StringBuilder> sbs = new List<StringBuilder>();
                for (int j = 1; j < arrays[0][0].Length - 1; j++)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int k = 0; k < arrays.Count; k++)
                    {
                        string word = new string(arrays[k][j]).Substring(1, 8);
                        sb.Append(word);
                    }

                    sbs.Add(sb);
                }

                for (int j = 0; j < sbs.Count; j++)
                {
                    grid[index++] = sbs[j].ToString().ToArray();
                }
            }

            long ans = 0;
            for (int i = 0; i <= 8; i++)
            {
                char[][] copy = (char[][])grid.Clone();
                int sea = 0;
                for (int j = 0; j < copy.Length - monster.Count; j++)
                {
                    for (int k = 0; k < copy[j].Length - monster[0].Length; k++)
                    {
                        int count = 0;
                        List<(int x, int y)> coords = new List<(int, int)>();
                        for (int x = 0; x < monster.Count; x++)
                        {
                            for (int m = 0; m < monster[x].Length; m++)
                            {
                                int _x = j + x;
                                int _y = k + m;

                                if (monster[x][m] == '#' && copy[_x][_y] == monster[x][m])
                                {
                                    coords.Add((_x, _y));
                                    count++;
                                }
                            }
                        }

                        if (count == 15)
                        {
                            foreach (var (x, y) in coords)
                            {
                                copy[x][y] = 'O';
                            }

                            sea++;
                        }
                    }
                }

                if (sea > 0)
                {
                    Console.WriteLine("Monters: " + sea);
                    int temp = 0;
                    foreach (var row in copy)
                    {
                        Console.WriteLine(string.Join("", row));
                        temp += row.Count(c => c == '#');
                    }
                    ans = Math.Max(ans, temp);
                }

                grid = Rotate(copy);
                if (i == 4)
                {
                    grid = Flip(copy);
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private List<List<char[][]>> GenerateImage(int size)
        {
            int n = grids.Count;

            for (int index = 0; index < n; index++)
            {
                List<List<char[][]>> puzzle = new List<List<char[][]>>();

                for (int x = 0; x < size; x++)
                {
                    puzzle.Add(new List<char[][]>());
                }

                var corner = grids[index];
                puzzle[0].Add(corner);

                HashSet<long> isVisited = new HashSet<long>
                {
                    ids[corner]
                };

                int i = 0, nextIndex = 0;
                while (i != size && nextIndex != n)
                {
                    var current = grids[nextIndex];
                    while (isVisited.Contains(ids[current]))
                    {
                        nextIndex++;

                        if (nextIndex >= n) break;

                        current = grids[nextIndex];
                    }

                    if (nextIndex >= n) break;

                    bool isMatch = true;
                    if (i > 0 && puzzle[i].Count == 0) //Upper check
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (puzzle[i - 1].First().Last()[j] != current.First()[j])
                            {
                                isMatch = false;
                                break;
                            }
                        }
                    }
                    else if (i > 0 && puzzle[i].Count > 0) //Left check && Upper check
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (puzzle[i].Last()[j].Last() != current[j].First() || //Left
                                puzzle[i - 1][puzzle[i].Count].Last()[j] != current.First()[j]) //Upper
                            {
                                isMatch = false;
                                break;
                            }
                        }
                    }
                    else if (puzzle[i].Count > 0) //Left check
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (puzzle[i].Last()[j].Last() != current[j].First())
                            {
                                isMatch = false;
                                break;
                            }
                        }
                    }

                    if (isMatch)
                    {
                        puzzle[i].Add(current);
                        isVisited.Add(ids[current]);
                        nextIndex = 0;

                        if (puzzle[i].Count == size)
                        {
                            i++;
                        }

                        if (i == size)
                        {
                            //for (int j = 0; j < size; j++)
                            //{
                            //    for (int k = 0; k < size; k++)
                            //    {
                            //        Console.Write(ids[puzzle[j][k]] + " ");
                            //    }
                            //    Console.WriteLine();
                            //}

                            return puzzle;
                        }
                    }
                    else
                    {
                        nextIndex++;
                    }
                }
            }

            return null;
        }

        private char[][] GenerateGrid(int size)
        {
            char[][] grid = new char[size][];
            for (int i = 0; i < size; i++)
            {
                char[] width = new char[size];
                grid[i] = width;
            }
            return grid;
        }

        private char[][] Rotate(char[][] grid)
        {
            char[][] temp = GenerateGrid(grid.Length);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    temp[j][temp.Length - 1 - i] = grid[i][j];
                }
            }
            return temp;
        }

        private char[][] Flip(char[][] grid)
        {
            char[][] flip = GenerateGrid(grid.Length);
            int position = 0;
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                flip[position++] = grid[i];
            }
            return flip;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2020\Day 20\input.txt";
            var lines = File.ReadAllLines(path).ToList();
            lines.Add(string.Empty);

            //10x10
            char[][] grid = GenerateGrid(10);
            int index = 0;
            int counter = 0;
            long currentID = 0;
            foreach (var s in lines)
            {
                if (string.IsNullOrEmpty(s))
                {
                    grids.Add(index, grid);
                    ids.Add(grid, currentID);

                    for (int i = 0; i < 3; i++)
                    {
                        var last = grids[index++];
                        char[][] temp = Rotate(last);
                        grids.Add(index, temp);
                        ids.Add(temp, currentID);
                    }

                    index++;
                    char[][] flip = Flip(grid);
                    grids.Add(index, flip);
                    ids.Add(flip, currentID);

                    for (int i = 0; i < 3; i++)
                    {
                        var last = grids[index++];
                        char[][] temp = Rotate(last);
                        grids.Add(index, temp);
                        ids.Add(temp, currentID);
                    }

                    index++;
                    counter = 0;
                    grid = GenerateGrid(10);
                }
                else if (!s.Contains(" "))
                {
                    grid[counter++] = s.ToArray();
                }
                else
                {
                    var split = s.Split(' ');
                    currentID = long.Parse(split[1].Substring(0, 4));
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day20();
        }
    }
}
