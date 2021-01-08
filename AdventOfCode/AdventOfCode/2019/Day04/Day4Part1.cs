using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day04
{
    public class Day4Part1
    {
        private int min, max;

        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            for (int i = 0; i < max - min; i++)
            {
                string num = (min + i).ToString();

                int n = num.Length;
                bool isNext = false;
                for (int j = 0; j < n - 1; j++)
                {
                    if (num[j] == num[j + 1])
                    {
                        isNext = true;
                        break;
                    }
                }

                if (!isNext) continue;

                bool isIncrease = true;
                for (int j = 0; j < n && isIncrease; j++)
                {
                    for (int k = j + 1; k < n && isIncrease; k++)
                    {
                        if (int.Parse(num[j].ToString()) > int.Parse(num[k].ToString()))
                        {
                            isIncrease = false;
                        }
                    }
                }

                if (!isIncrease) continue;

                ans++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2019\Day04\input.txt";
            var input = File.ReadAllLines(path)[0].Split('-').Select(int.Parse).ToList();
            min = input[0];
            max = input[1];
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
