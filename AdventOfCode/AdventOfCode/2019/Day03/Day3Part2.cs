using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day03
{
    public class Day3Part2
    {
        private readonly List<List<Action>> actions = new List<List<Action>>();

        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Dictionary<(int x, int y), Drawing> grid = new Dictionary<(int x, int y), Drawing>();
            foreach (var list in actions)
            {
                int x = 0, y = 0;
                foreach (var action in list)
                {
                    for (int i = 0; i < action.Steps; i++)
                    {
                        switch (action.Direction)
                        {
                            case Direction.RIGHT:
                                y++;
                                break;
                            case Direction.LEFT:
                                y--;
                                break;
                            case Direction.UP:
                                x--;
                                break;
                            case Direction.DOWN:
                                x++;
                                break;
                        }

                        if (!grid.ContainsKey((x, y)))
                        {
                            grid.Add((x, y), Drawing.ROAD);
                        }
                        else
                        {
                            grid[(x, y)] = Drawing.TURN;
                        }
                    }
                }
            }

            foreach (var (x, y) in grid.Keys.ToList())
            {
                if (IsIntersection(grid, x, y))
                {
                    grid[(x, y)] = Drawing.INTERSECTION;
                }
            }

            List<Dictionary<(int x, int y), int>> wires = new List<Dictionary<(int x, int y), int>>();            
            foreach (var list in actions)
            {
                Dictionary<(int x, int y), int> intersectionPoints = new Dictionary<(int x, int y), int>();
                int x = 0, y = 0, steps = 0;
                foreach (var action in list)
                {
                    for (int i = 0; i < action.Steps; i++)
                    {
                        steps++;

                        switch (action.Direction)
                        {
                            case Direction.RIGHT:
                                y++;
                                break;
                            case Direction.LEFT:
                                y--;
                                break;
                            case Direction.UP:
                                x--;
                                break;
                            case Direction.DOWN:
                                x++;
                                break;
                        }

                        if (IsIntersection(grid, x, y) && !intersectionPoints.ContainsKey((x, y)))
                        {
                            intersectionPoints.Add((x, y), steps);
                        }
                    }
                }
                wires.Add(intersectionPoints);
            }

            int ans = int.MaxValue;
            foreach (var kv in wires.First())
            {
                if (wires.Last().ContainsKey((kv.Key.x, kv.Key.y)))
                {
                    ans = Math.Min(ans, kv.Value + wires.Last()[(kv.Key.x, kv.Key.y)]);
                }                
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsIntersection(Dictionary<(int x, int y), Drawing> map, int x, int y)
        {
            return map.ContainsKey((x - 1, y)) && map.ContainsKey((x + 1, y)) && map.ContainsKey((x, y - 1)) && map.ContainsKey((x, y + 1));
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2019\Day03\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(',');
                List<Action> temp = new List<Action>();
                foreach (var value in split)
                {
                    var direction = (Direction)value[0];
                    var steps = int.Parse(value.Substring(1));
                    temp.Add(new Action()
                    {
                        Direction = direction,
                        Steps = steps
                    });
                }
                actions.Add(temp);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
