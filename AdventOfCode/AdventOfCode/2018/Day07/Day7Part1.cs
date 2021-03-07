using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day07
{
    public class Day7Part1
    {
        private readonly List<(string requirement, string next)> actions = new List<(string requirement, string next)>();
        private HashSet<string> uniqueSteps = new HashSet<string>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StringBuilder ans = new StringBuilder();
            while (uniqueSteps.Any())
            {
                var current = uniqueSteps.Where(s => !actions.Any(d => d.next.Equals(s))).First();
                ans.Append(current);
                uniqueSteps.Remove(current);
                actions.RemoveAll(d => d.requirement.Equals(current));
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day07\input.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(' ')).ToList();

            foreach (var array in lines)
            {
                var requirement = array[1];
                var next = array[7];

                uniqueSteps.Add(requirement);
                uniqueSteps.Add(next);

                actions.Add((requirement, next));
            }

            uniqueSteps = uniqueSteps.OrderBy(s => s).ToHashSet();
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
