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
        private readonly List<(string requirement, string next)> steps = new List<(string requirement, string next)>();
        private HashSet<string> uniqueSteps = new HashSet<string>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StringBuilder ans = new StringBuilder();
            while (uniqueSteps.Any())
            {
                var current = uniqueSteps.Where(s => !steps.Any(r => r.next == s)).First();
                ans.Append(current);
                steps.RemoveAll(s => s.requirement == current);
                uniqueSteps.Remove(current);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2018\Day07\sample.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(' ')).ToList();

            foreach (var array in lines)
            {
                var requirement = array[1];
                var next = array[7];
                steps.Add((requirement, next));
                uniqueSteps.Add(requirement);
                uniqueSteps.Add(next);
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
