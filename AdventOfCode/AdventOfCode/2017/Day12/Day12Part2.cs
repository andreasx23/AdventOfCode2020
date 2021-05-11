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

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            List<Pipe> pipes = map.Values.ToList();
            while (pipes.Any())
            {
                Pipe current = pipes.First();
                HashSet<Pipe> group = new HashSet<Pipe>();
                GenerateGroup(current, group);
                pipes.RemoveAll(p => group.Contains(p));
                ans++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void GenerateGroup(Pipe pipe, HashSet<Pipe> group)
        {
            if (group.Add(pipe))
            {
                foreach (var child in pipe.Connections)
                {
                    GenerateGroup(child, group);
                }
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2017\Day12\input.txt";
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
