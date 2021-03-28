using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day18
{
    public class Day18Part2
    {
        private readonly bool isSample = false;
        private string input;

        enum Value
        {
            Trap = '^',
            Safe = '.'
        }

        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            char[][] grid = new char[isSample ? 10 : 400000][];
            grid[0] = input.ToCharArray();

            int n = input.Length;
            for (int i = 1; i < grid.Length; i++)
            {
                grid[i] = new char[n];

                for (int j = 0; j < n; j++)
                {
                    char left = (j > 0) ? grid[i - 1][j - 1] : (char) Value.Safe;
                    char middle = grid[i - 1][j];
                    char right = (j + 1 < n) ? grid[i - 1][j + 1] : (char)Value.Safe;

                    grid[i][j] = GetTileValue(left, middle, right);
                }
            }

            int ans = 0;
            foreach (var arr in grid)
            {
                ans += arr.Count(c => c == '.');
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char GetTileValue(char left, char middle, char right)
        {
            if (left == (char)Value.Trap && middle == (char)Value.Trap && right == (char)Value.Safe ||
                left == (char)Value.Safe && middle == (char)Value.Trap && right == (char)Value.Trap ||
                left == (char)Value.Trap && middle == (char)Value.Safe && right == (char)Value.Safe ||
                left == (char)Value.Safe && middle == (char)Value.Safe && right == (char)Value.Trap)
            {
                return (char)Value.Trap;
            }

            return (char)Value.Safe;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day18\input.txt";
            input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
