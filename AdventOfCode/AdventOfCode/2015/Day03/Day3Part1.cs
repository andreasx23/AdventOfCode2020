using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day03
{
    public class Day3Part1
    {
        private List<Direction> directions = new List<Direction>();

        enum Direction
        {
            Left = '<',
            Right = '>',
            Up = '^',
            Down = 'v'
        }
        
        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            HashSet<(int x, int y)> set = new HashSet<(int x, int y)>();
            int x = 0, y = 0;
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case Direction.Left:
                        y--;
                        break;
                    case Direction.Right:
                        y++;
                        break;
                    case Direction.Up:
                        x--;
                        break;
                    case Direction.Down:
                        x++;
                        break;
                    default:
                        throw new Exception();
                }
                set.Add((x, y));
            }

            int ans = set.Count;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day03\input.txt";
            directions = File.ReadAllLines(path).First().Select(c => (Direction)c).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
