using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day16
{
    public class Day16Part2
    {
        private static readonly bool isSample = false;
        private readonly string input = (isSample) ? "10000" : "10111011111001111";
        private readonly int len = (isSample) ? 20 : 35651584;

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = Dissect(DragonCurve(input));

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string Dissect(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2)
            {
                string sub = input.Substring(i, 2);
                sb.Append(sub.Equals("00") || sub.Equals("11") ? '1' : '0');
            }

            if (sb.Length % 2 == 1)
            {
                return sb.ToString();
            }
            else
            {
                return Dissect(sb.ToString());
            }
        }

        private string DragonCurve(string a)
        {
            char[] b = a.Reverse().ToArray();
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = (b[i] == '0') ? '1' : '0';
            }

            string current = $"{a}0{new string(b)}";
            if (current.Length >= len)
            {
                return current.Substring(0, len);
            }
            else
            {
                return DragonCurve(current);
            }
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day12\input.txt";
            //input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
