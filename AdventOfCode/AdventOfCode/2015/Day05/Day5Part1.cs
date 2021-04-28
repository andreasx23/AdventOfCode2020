using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day05
{
    public class Day5Part1
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
            List<string> check3 = new List<string>() { "ab", "cd", "pq", "xy" };
            foreach (var s in check3)
            {
                if (input.Contains(s)) return false; 
            }

            List<char> check1 = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
            if (input.Count(c => check1.Contains(c)) < 3) return false;

            for (int i = 1; i < input.Length; i++)
            {
                char left = input[i - 1], middle = input[i];
                if (left == middle) return true; 
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
