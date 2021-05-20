using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day08
{
    public class Day8Part2
    {
        private string input = string.Empty;
        private readonly int H = 6, W = 25;

        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<List<char>> lists = new List<List<char>>();
            List<char> current = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (i > 0 && i % (H * W) == 0)
                {
                    lists.Add(current);
                    current = new List<char>() { c };
                }
                else
                {
                    current.Add(c);
                }
            }
            lists.Add(current);

            List<char> final = new List<char>();
            int n = lists.First().Count;
            for (int i = 0; i < n; i++)
            {
                int index = 0;
                while (lists[index][i] == '2')
                {
                    index++;
                }
                final.Add(lists[index][i] == '1' ? '#' : ' ');
            }

            for (int i = 0; i < n; i++)
            {
                Console.Write(final[i]);
                if ((i + 1) % W == 0)
                {
                    Console.WriteLine();
                }
            }

            int ans = 0;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day08\input.txt";
            input = File.ReadAllLines(path).First();
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
