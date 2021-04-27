using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day09
{
    public class Day9Part2
    {
        private readonly List<Node> nodes = new List<Node>();

        private void Day9()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Queue<State> queue = new Queue<State>();
            foreach (var n in nodes)
            {
                HashSet<Node> visisted = new HashSet<Node>() { n };
                State state = new State()
                {
                    Score = 0,
                    Node = n,
                    Visited = visisted
                };
                queue.Enqueue(state);
            }

            int ans = int.MinValue;
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Visited.Count == nodes.Count)
                {
                    ans = Math.Max(ans, current.Score);
                    continue;
                }

                foreach (var (neighbour, distance) in current.Node.Edges)
                {
                    if (!current.Visited.Contains(neighbour))
                    {
                        HashSet<Node> visited = new HashSet<Node>(current.Visited)
                        {
                            neighbour
                        };

                        State state = new State()
                        {
                            Score = current.Score + distance,
                            Node = neighbour,
                            Visited = visited
                        };

                        queue.Enqueue(state);
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day09\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " to ", " = " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var left = nodes.FirstOrDefault(n => n.Name == split[0]);
                var right = nodes.FirstOrDefault(n => n.Name == split[1]);
                var distance = int.Parse(split[2]);

                if (left != null && right != null)
                {
                    if (!left.Edges.Contains((right, distance)))
                    {
                        left.Edges.Add((right, distance));
                    }

                    if (!right.Edges.Contains((left, distance)))
                    {
                        right.Edges.Add((left, distance));
                    }
                }
                else if (left != null && right == null)
                {
                    right = new Node()
                    {
                        Name = split[1]
                    };
                    right.Edges.Add((left, distance));

                    if (!left.Edges.Contains((right, distance)))
                    {
                        left.Edges.Add((right, distance));
                    }

                    nodes.Add(right);
                }
                else if (left == null && right != null)
                {
                    left = new Node()
                    {
                        Name = split[0]
                    };
                    left.Edges.Add((right, distance));

                    if (!right.Edges.Contains((left, distance)))
                    {
                        right.Edges.Add((left, distance));
                    }

                    nodes.Add(left);
                }
                else if (left == null && right == null)
                {
                    left = new Node()
                    {
                        Name = split[0]
                    };

                    right = new Node()
                    {
                        Name = split[1]
                    };

                    left.Edges.Add((right, distance));
                    right.Edges.Add((left, distance));

                    nodes.Add(left);
                    nodes.Add(right);
                }
                else
                {
                    Console.WriteLine("ERROR!");
                    break;
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
