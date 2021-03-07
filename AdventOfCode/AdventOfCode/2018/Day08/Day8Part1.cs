using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day08
{
    public class Day8Part1
    {
        class Node
        {
            public int Value;
            public Node Left;
            public Node Right;
            public List<int> Metadata;

            public Node()
            {
                Metadata = new List<int>();
            }
        }

        private readonly List<List<int>> input = new List<List<int>>();

        //Not solved
        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var item in input)
            {
                Console.WriteLine(string.Join(" ", item));
            }

            int ans = 0;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day08\sample.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(' ').Select(int.Parse).ToList()).ToList();
            input.AddRange(lines);
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
