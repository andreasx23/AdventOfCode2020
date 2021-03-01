using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day02
{
    public class Day2Part1
    {
        private List<string> input = new List<string>();

        private void Day2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int two = 0, three = 0;
            foreach (var s in input)
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
                        two++;
                        isTwo = true;
                    }
                    else if (!isThree && kv.Value == 3)
                    {
                        three++;
                        isThree = true;
                    }

                    if (isTwo && isThree)
                    {
                        break;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {two * three} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day02\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day2();
        }
    }
}
