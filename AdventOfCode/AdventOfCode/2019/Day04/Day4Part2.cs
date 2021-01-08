using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day04
{
    public class Day4Part2
    {
        private int min, max;

        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //ans > 1141 && ans < 1215
            int ans = 0;
            for (int i = 0; i < max - min; i++)
            {
                string num = (min + i).ToString();
                int n = num.Length;

                Dictionary<char, int> count = new Dictionary<char, int>();
                bool isNext = true;
                for (int j = 0; j < n - 1; j++)
                {
                    if (num[j] == num[j + 1])
                    {
                        char temp = num[j];

                        if (!count.ContainsKey(temp)) count.Add(temp, 0);
                        count[temp] += 2;

                        j += 2;
                        while (j != n && num[j] == temp)
                        {
                            count[temp]++;
                            j++;
                        }

                        if (count[temp] % 2 > 0)
                        {
                            isNext = false;
                        }
                        else
                        {
                            isNext = true;
                        }
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

                if (isNext && isIncrease) ans++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day04\input.txt";
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
