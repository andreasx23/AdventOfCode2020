using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day17
{
    public class Day17Part1
    {
        private List<int> input = new List<int>();
        private static readonly bool isSample = false;
        private readonly int amount = isSample ? 25 : 150;
        private int globalAns = 0;

        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Dfs(0, 0);
            int ans = globalAns;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Dfs(int index, int sum)
        {
            if (sum == amount)
            {
                globalAns++;
                return;
            }

            for (int i = index; i < input.Count; i++)
            {
                Dfs(i + 1, sum + input[i]);
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day17\input.txt";
            input = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day17();
        }
    }
}
