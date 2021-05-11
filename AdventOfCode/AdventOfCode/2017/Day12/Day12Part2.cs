using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day12
{
    public class Day12Part2
    {
        private readonly Dictionary<int, Pipe> map = new Dictionary<int, Pipe>();
        private int ans = 0;

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var pipe in map.Values)
            {
                DFS(pipe, 0, new HashSet<Pipe>());
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void DFS(Pipe pipe, int target, HashSet<Pipe> isVisited)
        {
            if (pipe.Id == target)
            {
                ans++;
                return;
            }

            if (isVisited.Add(pipe))
            {
                foreach (var connection in pipe.Connections)
                {
                    DFS(connection, target, isVisited);
                }
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day12\sample.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var id = int.Parse(split.First());
                Pipe pipe = new Pipe()
                {
                    Id = id
                };

                map.Add(id, pipe);
            }

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var id = int.Parse(split.First());
                Pipe pipe = map[id];
                foreach (var connection in split.Last().Split(','))
                {
                    var connectionId = int.Parse(connection);

                    var connectionPipe = map[connectionId];
                    pipe.Connections.Add(connectionPipe);
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
