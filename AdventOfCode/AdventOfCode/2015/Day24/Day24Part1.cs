using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day24
{
    public class Day24Part1
    {
        private List<int> input = new List<int>();
        private readonly List<List<int>> matches = new List<List<int>>();

        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int sum = input.Sum();
            Recursion(0, sum / 3, new List<int>(), 0);

            var min = matches.Min(m => m.Count);
            var where = matches.Where(m => m.Count == min);

            int ans = int.MaxValue;
            foreach (var list in where)
            {
                var prod = 1;
                foreach (var v in list)
                {
                    prod *= v;
                }
                ans = Math.Min(ans, prod);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Recursion(int index, int target, List<int> values, long sum)
        {
            if (sum == target)
            {
                matches.Add(new List<int>(values));
                return;
            }

            for (int i = index; i < input.Count; i++)
            {
                values.Add(input[i]);
                Recursion(index + 1, target, values, sum + input[i]);
                values.RemoveAt(values.Count - 1);
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day24\sample.txt";
            input = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
