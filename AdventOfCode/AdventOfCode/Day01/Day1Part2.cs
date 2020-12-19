using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1
{
    public class Day1Part2
    {
        private List<int> input = new List<int>();

        public int Get2020()
        {
            int n = input.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        int numI = input[i], numJ = input[j], numK = input[k];
                        int sum = numI + numJ + numK;
                        if (sum == 2020)
                        {
                            Console.WriteLine(numI + " " + numJ + " " + numK);
                            return numI * numJ * numK;
                        }
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
