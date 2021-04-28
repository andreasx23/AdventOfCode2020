using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day05
{
    public class Day5Part2
    {
        private List<string> inputs = new List<string>();

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = inputs.Count(s => NiceString(s));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool NiceString(string input)
        {
            int n = input.Length;

            bool check1 = false;
            for (int i = 1; i < n && !check1; i++)
            {
                string pair = input[i - 1].ToString() + input[i].ToString();
                for (int j = i + 2; j < n && !check1; j++)
                {
                    string check = input[j - 1].ToString() + input[j].ToString();
                    if (pair == check) check1 = true;
                }
            }
            if (!check1) return false;

            for (int i = 0; i < n - 2; i++)
            {
                char current = input[i], match = input[i + 2];
                if (current == match) return true;
            }

            return false;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day05\input.txt";
            inputs = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
