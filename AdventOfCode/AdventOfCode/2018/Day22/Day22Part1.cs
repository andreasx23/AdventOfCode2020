using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day22
{
    public class Day22Part1
    {
        class Coordinate
        {
            public int X;
            public int Y;
            public char Type;
        }

        private int H = 0, W = 0, depth = 0, mod = 20183;
        private readonly Coordinate start = new Coordinate() { X = 0, Y = 0, Type = 'M' };
        private readonly Coordinate target = new Coordinate() { Type = 'T' };

        //https://adventofcode.com/2018/day/22
        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (depth > 510)
            {
                H = 800;
                W = 15;
            }
            else
            {
                H = 16;
                W = 16;
            }
            
            long[][] dp = new long[H][];
            char[][] grid = new char[H][];
            for (int x = 0; x < H; x++)
            {
                grid[x] = new char[W];
                dp[x] = new long[W];
            }

            dp[start.X][start.Y] = 0;
            dp[target.X][target.Y] = 0;

            for (int i = 1; i < H; i++)
            {
                long sum = i * 48271 + depth;
                long erosionLevel = sum % mod;
                dp[i][0] = erosionLevel;
            }

            for (int i = 1; i < W; i++)
            {
                long sum = i * 16807 + depth;
                long erosionLevel = sum % mod;
                dp[0][i] = erosionLevel;
            }

            for (int i = 1; i < H; i++)
            {
                for (int j = 1; j < W; j++)
                {
                    if (i == target.X && j == target.Y) continue;

                    long sum = dp[i - 1][j] * dp[i][j - 1] + depth;
                    long erosionLevel = sum % mod;
                    dp[i][j] = erosionLevel;

                    //Console.WriteLine($"{map[(x - 1, y)]}*{map[(x, y - 1)]}+{depth}={sum}");
                    //Console.WriteLine("Erosion level: " + erosionLevel);
                    //return;
                }
            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    long result = dp[i][j] % 3;
                    if (result == 0)
                    {
                        grid[i][j] = '.'; //Rocky
                    }
                    else if (result == 1)
                    {
                        grid[i][j] = '='; //Wet
                    }
                    else if (result == 2)
                    {
                        grid[i][j] = '|'; //Narrow
                    }
                }
            }

            long temp = depth % mod;
            temp %= 3;
            long ans = temp * 2;
            for (int i = 0; i <= target.X; i++)
            {
                for (int j = 0; j <= target.Y; j++)
                {
                    //Console.WriteLine(i + " " + j + " : " + longs[i][j]);

                    if (grid[i][j] == '.')//Rocky
                    {
                        ans += 0;
                    }
                    else if (grid[i][j] == '=') //Wet
                    {
                        ans += 1;
                    }
                    else if (grid[i][j] == '|') //Narrow
                    {
                        ans += 2;
                    }
                }
            }

            //grid[start.X][start.Y] = start.Type;
            //grid[target.X][target.Y] = target.Type;

            Print(grid);
            Console.WriteLine();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print(char[][] grid)
        {
            int index = 0;
            foreach (var item in grid)
            {
                Console.WriteLine(index++ + " " + string.Join("", item));
            }
        }

        private void ReadData()
        {
            const string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day22\input.txt";
            var lines = File.ReadAllLines(path);
            var first = lines[0].Split(' ');
            var depth = int.Parse(first[1]);
            this.depth = depth;
            var second = lines[1].Split(' ')[1].Split(',');
            var X = int.Parse(second[1]);
            var Y = int.Parse(second[0]);
            target.X = X;
            target.Y = Y;

            string path2 = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day22\output.txt";
            lines = File.ReadAllLines(path2);
            int index = 0;
            foreach (var item in lines)
            {
                Console.WriteLine($"{index++} {item}");
            }
            Console.WriteLine();
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}
