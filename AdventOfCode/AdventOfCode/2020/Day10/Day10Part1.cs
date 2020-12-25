using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    public class Day10Part1
    {
        private List<int> input = new List<int>();

        private void Day10()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            input.Insert(0, 0);
            input.Add(input.Last() + 3);

            int oneJolt = 0, threeJolt = 0;

            int sum = 0;
            foreach (var n in input)
            {
                int calc = n - sum;
                sum += calc;

                if (calc == 1)
                {
                    oneJolt++;
                }
                else if (calc == 3)
                {
                    threeJolt++;
                }
            }
            int ans = oneJolt * threeJolt;

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 10\sample.txt";
            input = File.ReadAllLines(path).Select(s => int.Parse(s)).OrderBy(n => n).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day10();
        }
    }
}
