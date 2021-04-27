using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day06
{
    public class Day6Part1
    {
        private List<string> orders = new List<string>();

        enum Type
        {
            TurnOn = 1,
            TurnOff = 2,
            Toggle = 3
        }

        private void Day6()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int H = 1000, W = 1000;
            int[][] grid = new int[H][];
            for (int i = 0; i < H; i++)
            {
                grid[i] = new int[W];
            }

            foreach (var order in orders)
            {
                var (type, startX, startY, targetX, targetY) = GetOrder(order);

                for (int x = startX; x <= targetX; x++)
                {
                    for (int y = startY; y <= targetY; y++)
                    {
                        if (type == Type.TurnOn)
                        {
                            grid[x][y] = 1;
                        }
                        else if (type == Type.TurnOff)
                        {
                            grid[x][y] = 0;
                        }
                        else
                        {
                            grid[x][y] = (grid[x][y] == 1) ? 0 : 1;
                        }
                    }
                }
            }

            int ans = grid.Select(array => array.Sum()).Sum();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private (Type type, int startX, int startY, int targetX, int targetY) GetOrder(string command)
        {
            Type type;
            if (command.Contains("on"))
            {
                type = Type.TurnOn;
            }
            else if (command.Contains("off"))
            {
                type = Type.TurnOff;
            }
            else //toggle
            {
                type = Type.Toggle;
            }

            command = command.Replace("turn on ", "").Replace("turn off ", "").Replace("toggle ", "");
            List<string> split = command.Split(new string[] { " through " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> start = split.First().Split(',').Select(int.Parse).ToList();
            List<int> target = split.Last().Split(',').Select(int.Parse).ToList();
            int startX = start.Last();
            int startY = start.First();
            int targetX = target.Last();
            int targetY = target.First();

            return (type, startX, startY, targetX, targetY);
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day06\input.txt";
            orders = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
