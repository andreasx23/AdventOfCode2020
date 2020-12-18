using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Day1
{
    public class Day1Part1
    {
        private List<int> input = new List<int>();

        public int Get2020()
        {
            int n = input.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int numI = input[i], numJ = input[j];
                    int sum = numI + numJ;
                    if (sum == 2020)
                    {
                        return numI * numJ;
                    }
                }
            }

            return -1;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 1\input.txt";
            input = File.ReadAllLines(path).Select(r => int.Parse(r)).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Console.WriteLine("Expected: 2020");
            var result = Get2020();
            Console.WriteLine(result);
        }
    }
}
