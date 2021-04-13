using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day17
{
    public class Day17Part2
    {
        private List<int> input = new List<int>();
        private static readonly bool isSample = false;
        private readonly int amount = isSample ? 25 : 150;
        private int globalAns = 0;

        private void Day17()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = int.MaxValue;
            for (int i = 1; i <= input.Count; i++)
            {
                Dfs(0, 0, 0, i);

                if (globalAns > 0)
                {
                    ans = globalAns;
                    break;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Dfs(int index, int sum, int containers, int maxContainer)
        {
            if (sum == amount)
            {
                globalAns++;
                return;
            }

            if (containers == maxContainer) 
            {
                return;
            }

            for (int i = index; i < input.Count; i++)
            {
                Dfs(i + 1, sum + input[i], containers + 1, maxContainer);
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
