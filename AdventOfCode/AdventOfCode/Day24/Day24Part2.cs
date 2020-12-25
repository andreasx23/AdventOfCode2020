using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    public class Day24Part2
    {
        private Dictionary<(int x, int y), bool> grid = new Dictionary<(int x, int y), bool>();
        private readonly Dictionary<string, (int i, int j)> dirs = new Dictionary<string, (int i, int j)>()
        {
            { "ne", (0, 1) },
            { "sw", (0, -1) },
            { "e", (1, 0) },
            { "w", (-1, 0) },
            { "nw", (-1, 1) },
            { "se", (1, -1) }
        };

        /*
         * Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
         * Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
         * 
         * Unsolved
         */
        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Console.WriteLine("Day 1: " + grid.Count(kv => kv.Value));

            int ans = 0;
            for (int i = 1; i < 100; i++)
            {
                var copy = new Dictionary<(int x, int y), bool>(grid);

                foreach (var tile in grid)
                {
                    var key = tile.Key;
                    //Console.WriteLine($"Current tile: {key.x} {key.y}");

                    Dictionary<(int x, int y), bool> neighbours = new Dictionary<(int x, int y), bool>();
                    foreach (var value in dirs.Values)
                    {
                        int newX = key.x + value.i;
                        int newY = key.y + value.j;
                        //Console.WriteLine($"Looking for neighbour at: {newX} {newY}");

                        if (grid.ContainsKey((newX, newY)))
                        {
                            //Console.WriteLine("Neighbour found:");
                            neighbours.Add((newX, newY), grid[(newX, newY)]);
                        }
                        else
                        {
                            if (!copy.ContainsKey((newX, newY)))
                            {
                                copy.Add((newX, newY), false);
                            }
                        }
                    }

                    var count = neighbours.Count(kv => kv.Value);
                    if (tile.Value) //Black tile
                    {
                        if (count == 0 || count > 2)
                        {
                            copy[(key.x, key.y)] = false;
                        }
                    }
                    else //White tile
                    {   
                        if (count == 2)
                        {
                            copy[(key.x, key.y)] = true;
                        }
                    }
                }

                grid = new Dictionary<(int x, int y), bool>(copy);
                //ans = grid.Count(kv => kv.Value);
                //Console.WriteLine($"Day {i + 1}: {ans}");
            }
            ans = grid.Count(kv => kv.Value);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 24\sample.txt";
            var input = File.ReadAllLines(path);

            foreach (var s in input)
            {
                int i = 0, j = 0;
                
                for (int k = 0; k < s.Length; k++)
                {
                    StringBuilder sb = new StringBuilder();
                    char c = s[k];
                    sb.Append(c);
                    if (c == 's' || c == 'n')
                    {
                        k++;
                        c = s[k];
                        sb.Append(c);
                    }

                    string dir = sb.ToString();
                    var tuple = dirs[dir];
                    i += tuple.i;
                    j += tuple.j;
                }

                if (!grid.ContainsKey((i, j)))
                {
                    grid.Add((i, j), true);
                }
                else
                {
                    grid[(i, j)] = false;
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
