using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day24
{
    public class Day24Part2
    {
        private int[] inputs;

        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            BigInteger ans = LowestQuantumEntanglement(inputs, inputs.Sum() / 4, 0, 0, 1);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private BigInteger LowestQuantumEntanglement(int[] inputs, int target, int index, BigInteger sum, BigInteger productSum)
        {
            if (sum == target)
            {
                return productSum;
            }
            else if (index >= inputs.Length || sum > target)
            {
                return -1;
            }

            BigInteger lhs = LowestQuantumEntanglement(inputs, target, index + 1, sum + inputs[index], productSum * inputs[index]);
            BigInteger rhs = LowestQuantumEntanglement(inputs, target, index + 1, sum, productSum);

            if (lhs == -1) return rhs;
            if (rhs == -1) return lhs;

            return BigInteger.Min(lhs, rhs);
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2015\Day24\input.txt";
            inputs = File.ReadAllLines(path).Select(int.Parse).ToArray();
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
