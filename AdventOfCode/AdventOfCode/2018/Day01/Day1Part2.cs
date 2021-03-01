using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day01
{
    public class Day1Part2
    {
        private List<int> input = new List<int>();

        private void Day1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long sum = 0;
            int index = 0;
            HashSet<long> answers = new HashSet<long>();
            while (true)
            {
                sum += input[index];
                
                if (!answers.Add(sum))
                {
                    break;
                }

                index++;
                index %= input.Count;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {sum} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day01\input.txt";
            input = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day1();
        }
    }
}
