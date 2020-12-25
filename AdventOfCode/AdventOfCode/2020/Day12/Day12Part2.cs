using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace AdventOfCode.Day12
{
    public class Day12Part2
    {
        private List<string> input = new List<string>();

        //Starts by facing east
        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int east = 0, north = 0, eastWayPoint = 10, northWayPoint = 1;
            foreach (var s in input)
            {
                char instruction = s[0];
                int amount = int.Parse(s.Substring(1));

                switch (instruction)
                {
                    case 'L':
                        (eastWayPoint, northWayPoint) = Rotate(eastWayPoint, northWayPoint, amount);
                        break;
                    case 'R':
                        (eastWayPoint, northWayPoint) = Rotate(eastWayPoint, northWayPoint, -amount);
                        break;
                    case 'F':
                        east += eastWayPoint * amount;
                        north += northWayPoint * amount;
                        break;
                    case 'N':
                        northWayPoint += amount;
                        break;
                    case 'E':
                        eastWayPoint += amount;
                        break;
                    case 'S':
                        northWayPoint -= amount;
                        break;
                    case 'W':
                        eastWayPoint -= amount;
                        break;
                    default:
                        Console.WriteLine("Something went wrong");
                        return;
                }
            }
            int ans = Math.Abs(east) + Math.Abs(north);

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        //https://en.wikipedia.org/wiki/Rotation_matrix
        private (int, int) Rotate(int x, int y, int degrees)
        {
            float rads = degrees * ((float)Math.PI * 2 / 360);
            double newX = Math.Round(x * Math.Cos(rads) - y * Math.Sin(rads));
            double newY = Math.Round(x * Math.Sin(rads) + y * Math.Cos(rads));

            int newXParsed = int.Parse(newX.ToString());
            int newYParsed = int.Parse(newY.ToString());

            return (newXParsed, newYParsed);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 12\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
