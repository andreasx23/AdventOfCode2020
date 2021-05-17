using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day24
{
    public class Day24Part1
    {
        private readonly List<Port> ports = new List<Port>();
        private int bestScore = 0;

        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var port in ports.Where(p => p.Left == 0 || p.Right == 0))
            {
                if (port.Left == 0)
                {
                    port.IsLeftUsed = true;
                }
                else if (port.Right == 0)
                {
                    port.IsRightUsed = true;
                }

                var deep = ports.ConvertAll(p => new Port() { Left = p.Left, Right = p.Right, IsLeftUsed = p.IsLeftUsed, IsRightUsed = p.IsRightUsed });
                Recursive(deep, new HashSet<Port>() { port }, port, port.Score);

                port.IsLeftUsed = false;
                port.IsRightUsed = false;
            }

            int ans = bestScore;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Recursive(List<Port> ports, HashSet<Port> isVisited, Port current, int score)
        {
            foreach (var port in ports)
            {
                if (port.Left == current.Left && !current.IsLeftUsed && isVisited.Add(port) ||
                    port.Left == current.Right && !current.IsRightUsed && isVisited.Add(port))
                {
                    port.IsLeftUsed = true;
                    Recursive(ports, isVisited, port, score + port.Score);
                    port.IsLeftUsed = false;
                    isVisited.Remove(port);
                }
                else if (port.Right == current.Left && !current.IsLeftUsed && isVisited.Add(port) ||
                         port.Right == current.Right && !current.IsRightUsed && isVisited.Add(port))
                {
                    port.IsRightUsed = true;
                    Recursive(ports, isVisited, port, score + port.Score);
                    port.IsRightUsed = false;
                    isVisited.Remove(port);
                }
            }

            bestScore = Math.Max(bestScore, score);
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2017\Day24\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split('/');
                ports.Add(new Port()
                {
                    Left = int.Parse(split.First()),
                    Right = int.Parse(split.Last())
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
