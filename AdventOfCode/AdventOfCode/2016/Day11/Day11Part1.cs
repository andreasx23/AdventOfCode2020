using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day11
{
    public class Day11Part1
    {
        class Unit
        {
            public string name;
            public Unit compatible;
            public int x;
            public int y;
        }

        private readonly List<Unit> units = new List<Unit>();

        //https://adventofcode.com/2016/day/11 - Not solved
        private void Day11()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            char[][] grid = new char[4][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new char[units.Count];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = '.';
                }
            }

            //Can carry at max two items at a time
            int elavator = 0;
            //while (grid[3].Any(c => c == '.'))
            //{

            //}

            foreach (var item in units)
            {
                Console.WriteLine(item.name + " partner " + item.compatible.name);
            }

            int ans = 0;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day11\input.txt";
            var lines = File.ReadAllLines(path);

            var floorOne = lines[0].Replace("The first floor contains a ", "");
            var floorTwo = lines[1].Replace("The second floor contains a ", "");
            var flootThree = lines[2].Replace("The third floor contains a ", "");
            var floorFour = lines[3].Replace("The fourth floor contains ", "");

            List<string> floors = new List<string>()
            {
                floorOne,
                floorTwo,
                flootThree,
                floorFour
            };

            int x = 0, y = 0;
            foreach (var item in floors)
            {
                if (item.Contains("nothing")) 
                {
                    continue;
                }

                var split = item.Split(new string[] { " a " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var s in split)
                {
                    Unit unit = new Unit()
                    {
                        name = s.Replace(", and", "").Replace(" and", "").Replace(",", "").Replace(".", "").ToLower(),
                        x = x,
                        y = y++
                    };
                    units.Add(unit);
                }

                x++;
            }

            foreach (var unit in units)
            {
                foreach (var compatible in units)
                {
                    if (unit.name.Equals(compatible.name))
                    {
                        continue;
                    }

                    if (unit.name.Take(4).SequenceEqual(compatible.name.Take(4)))
                    {
                        unit.compatible = compatible;
                        break;
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day11();
        }
    }
}
