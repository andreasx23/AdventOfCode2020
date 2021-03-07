using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day11
{
    public class Day11Part2
    {
        class PowerCell
        {
            public int X { get; set; }
            public int Y { get; set; }
            public long RackID { get; private set; }
            public long PowerLevel { get; private set; }

            public void CalculatePowerlevel()
            {
                RackID = X + 1 + 10;
                PowerLevel = RackID * (Y + 1);
                PowerLevel += input; //Grid serial number
                PowerLevel *= RackID;

                long temp = 0;
                if (PowerLevel >= 100)
                {
                    temp = Convert.ToInt64(PowerLevel.ToString().Reverse().ToArray()[2].ToString());
                }
                temp -= 5;
                PowerLevel = temp;
            }
        }

        private static readonly int input = 7139;

        private void Day11()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<List<PowerCell>> powerCells = new List<List<PowerCell>>();

            int H = 300, W = 300;
            long[][] grid = new long[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = new long[W];

                List<PowerCell> temp = new List<PowerCell>();
                for (int j = 0; j < grid[i].Length; j++)
                {
                    PowerCell powerCell = new PowerCell()
                    {
                        X = i,
                        Y = j
                    };
                    powerCell.CalculatePowerlevel();
                    temp.Add(powerCell);
                    grid[i][j] = powerCell.PowerLevel;
                }
                powerCells.Add(temp);
            }

            long max = 0, bestX = 0, bestY = 0, size = 0;
            for (int y = 1; y <= H; y++)
            {
                Console.WriteLine("Round: " + y);
                for (int i = 0; i < H - y; i++)
                {
                    for (int j = 0; j < W - y; j++)
                    {
                        long sum = 0;
                        for (int k = 0; k < y; k++)
                        {
                            for (int x = 0; x < y; x++)
                            {
                                sum += grid[k + i][x + j];
                            }
                        }

                        if (sum > max)
                        {
                            max = sum;
                            bestX = i + 1;
                            bestY = j + 1;
                            size = y;
                        }
                    }
                }
                Console.WriteLine($"Current best: Sum:{max} X:{bestX},Y:{bestY} size:{size}");
                Console.WriteLine($"Possible answer output: {bestX},{bestY},{size}");
            }
            
            string ans = $"{bestX},{bestY},{size}";
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day10\sample.txt";
            var lines = File.ReadAllLines(path);
        }

        public void TestCase()
        {
            ReadData();
            Day11();
        }
    }
}
