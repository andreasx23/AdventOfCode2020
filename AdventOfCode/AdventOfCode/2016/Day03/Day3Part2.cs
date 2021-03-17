using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day03
{
    public class Day3Part2
    {
        private readonly List<List<int>> input = new List<List<int>>();

        private void Day3()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            for (int i = 0; i < input.Count; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    int a = input[i][j], b = input[i + 1][j], c = input[i + 2][j];

                    if (a + b > c && b + c > a && a + c > b)
                    {
                        ans++;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day03\input.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Trim());

            foreach (var s in lines)
            {
                string a = "", b = "", c = "";

                int i = 0;
                while (char.IsDigit(s[i]))
                {
                    a += s[i].ToString();
                    i++;
                }

                while (!char.IsDigit(s[i]))
                {
                    i++;
                }

                while (char.IsDigit(s[i]))
                {
                    b += s[i].ToString();
                    i++;
                }

                while (!char.IsDigit(s[i]))
                {
                    i++;
                }

                while (i != s.Length && char.IsDigit(s[i]))
                {
                    c += s[i].ToString();
                    i++;
                }

                var list = new List<int>() { int.Parse(a), int.Parse(b), int.Parse(c) };
                input.Add(list);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
