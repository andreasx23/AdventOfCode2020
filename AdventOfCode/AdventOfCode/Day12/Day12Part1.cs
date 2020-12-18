using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12
{
    public class Day12Part1
    {
        private List<string> input = new List<string>();

        //Starts by facing east
        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<char> directions = new List<char>() { 'N', 'E', 'S', 'W' };
            char currentDirection = directions[1];
            int north = 0, east = 0;
            foreach (var s in input)
            {
                char instruction = s[0];
                int amount = int.Parse(s.Substring(1));

                switch (instruction)
                {
                    case 'L':
                        {
                            int turns = amount / 90;
                            int index = directions.IndexOf(currentDirection);
                            for (int i = 0; i < turns; i++)
                            {
                                if (index == 0)
                                {
                                    index = directions.Count - 1;
                                }
                                else
                                {
                                    index--;
                                }
                            }
                            currentDirection = directions[index];
                        }                        
                        break;
                    case 'R':
                        {
                            int turns = amount / 90;
                            int index = directions.IndexOf(currentDirection);
                            for (int i = 0; i < turns; i++)
                            {
                                if (index + 1 == directions.Count)
                                {
                                    index = 0;
                                }
                                else
                                {
                                    index++;
                                }
                            }
                            currentDirection = directions[index];
                        }
                        break;
                    case 'F':
                        if (currentDirection == 'N')
                        {
                            north += amount;
                        }
                        else if (currentDirection == 'E')
                        {
                            east += amount;
                        }
                        else if (currentDirection == 'S')
                        {
                            north -= amount;
                        }
                        else if (currentDirection == 'W')
                        {
                            east -= amount;
                        }
                        break;
                    case 'N':
                        north += amount;
                        break;
                    case 'E':
                        east += amount;
                        break;
                    case 'S':
                        north -= amount;
                        break;
                    case 'W':
                        east -= amount;
                        break;
                    default:
                        Console.WriteLine("Something went wrong");
                        return;
                }
            }
            int ans = Math.Abs(north) + Math.Abs(east);

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
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
