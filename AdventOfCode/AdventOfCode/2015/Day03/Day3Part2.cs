using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day03
{
    public class Day3Part2
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
            int santaX = 0, santaY = 0, roboX = 0, roboY = 0;
            bool isSanta = true;
            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case Direction.Left when isSanta:
                        santaY--;
                        break;
                    case Direction.Left when !isSanta:
                        roboY--;
                        break;
                    case Direction.Right when isSanta:
                        santaY++;
                        break;
                    case Direction.Right when !isSanta:
                        roboY++;
                        break;
                    case Direction.Up when isSanta:
                        santaX--;
                        break;
                    case Direction.Up when !isSanta:
                        roboX--;
                        break;
                    case Direction.Down when isSanta:
                        santaX++;
                        break;
                    case Direction.Down when !isSanta:
                        roboX++;
                        break;
                    default:
                        throw new Exception();
                }

                if (isSanta)
                {
                    set.Add((santaX, santaY));
                }
                else
                {
                    set.Add((roboX, roboY));
                }

                isSanta = !isSanta;
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
