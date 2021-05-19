using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day03
{
    public class Day3Part1
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

            int ans = grid.Where(kv => kv.Value == Drawing.INTERSECTION).Min(kv => CalculateManhattenDistance(0, kv.Key.x, 0, kv.Key.y));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsIntersection(Dictionary<(int x, int y), Drawing> map, int x, int y)
        {
            return map.ContainsKey((x - 1, y)) && map.ContainsKey((x + 1, y)) && map.ContainsKey((x, y - 1)) && map.ContainsKey((x, y + 1));
        }

        private int CalculateManhattenDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
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
