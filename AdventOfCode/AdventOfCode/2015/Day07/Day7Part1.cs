using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day07
{
    public class Day7Part1
    {
        private readonly List<(string todo, string target)> commands = new List<(string todo, string target)>();
        private readonly Dictionary<string, ushort> map = new Dictionary<string, ushort>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            HashSet<string> isVisisted = new HashSet<string>();
            while (map["a"] == 0)
            {
                foreach (var (todo, target) in commands)
                {
                    ushort result;
                    if (todo.Contains("AND"))
                    {
                        var split = todo.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        ushort left = map.ContainsKey(split.First()) ? map[split.First()] : ushort.Parse(split.First());
                        ushort right = map.ContainsKey(split.Last()) ? map[split.Last()] : ushort.Parse(split.Last());

                        if (split.First().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.First()) ||
                            split.Last().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.Last())) continue;

                        result = (ushort)(left & right);
                    }
                    else if (todo.Contains("LSHIFT"))
                    {
                        var split = todo.Split(new string[] { " LSHIFT " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        ushort left = map.ContainsKey(split.First()) ? map[split.First()] : ushort.Parse(split.First());

                        if (split.First().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.First())) continue;

                        int amount = int.Parse(split.Last());
                        result = (ushort)(left << amount);
                    }
                    else if (todo.Contains("RSHIFT"))
                    {
                        var split = todo.Split(new string[] { " RSHIFT " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        ushort left = map.ContainsKey(split.First()) ? map[split.First()] : ushort.Parse(split.First());

                        if (split.First().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.First())) continue;

                        int amount = int.Parse(split.Last());
                        result = (ushort)(left >> amount);
                    }
                    else if (todo.Contains("NOT"))
                    {
                        var split = todo.Split(new string[] { "NOT " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        ushort right = map.ContainsKey(split.Last()) ? map[split.Last()] : ushort.Parse(split.Last());

                        if (split.Last().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.Last())) continue;

                        result = (ushort)~right;
                    }
                    else if (todo.Contains("OR"))
                    {
                        var split = todo.Split(new string[] { " OR " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        ushort left = map.ContainsKey(split.First()) ? map[split.First()] : ushort.Parse(split.First());
                        ushort right = map.ContainsKey(split.Last()) ? map[split.Last()] : ushort.Parse(split.Last());

                        if (split.First().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.First()) ||
                            split.Last().All(c => !char.IsDigit(c)) && !isVisisted.Contains(split.Last())) continue;

                        result = (ushort)(left | right);
                    }
                    else
                    {
                        var left = map.ContainsKey(todo) ? map[todo] : ushort.Parse(todo);

                        if (todo.All(c => !char.IsDigit(c)) && !isVisisted.Contains(todo)) continue;

                        result = left;                        
                    }

                    map[target] = result;
                    isVisisted.Add(target);
                }
            }

            ushort ans = map["a"];

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day07\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var todo = split.First();
                var target = split.Last();

                if (!map.ContainsKey(target))
                {
                    map.Add(target, 0);
                }

                commands.Add((todo, target));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
