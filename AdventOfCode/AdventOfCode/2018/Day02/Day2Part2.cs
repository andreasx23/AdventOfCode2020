using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day02
{
    public class Day2Part2
    {
        private readonly List<string> input = new List<string>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = "";
            bool isMatch = false;
            int n = input.Count, index = 0;
            for (int i = 0; i < n && !isMatch; i++)
            {
                string first = input[i];
                for (int j = i + 1; j < n && !isMatch; j++)
                {
                    string second = input[j];

                    int diff = 0;
                    for (int k = 0; k < first.Length; k++)
                    {
                        if (first[k] != second[k])
                        {
                            diff++;
                            index = k;
                        }
                    }

                    if (diff == 1)
                    {
                        ans = input[i].Remove(index, 1);
                        isMatch = true;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day02\input.txt";
            var lines = File.ReadAllLines(path).ToList();

            foreach (var s in lines)
            {
                Dictionary<char, int> map = new Dictionary<char, int>();
                foreach (var c in s)
                {
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c, 0);
                    }

                    map[c]++;
                }

                bool isTwo = false, isThree = false;
                foreach (var kv in map)
                {
                    if (!isTwo && kv.Value == 2)
                    {
                        isTwo = true;
                    }
                    else if (!isThree && kv.Value == 3)
                    {
                        isThree = true;
                    }

                    if (isTwo && isThree)
                    {
                        input.Add(s);
                        break;
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
