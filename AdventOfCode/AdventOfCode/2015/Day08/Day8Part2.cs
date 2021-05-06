using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day08
{
    public class Day8Part2
    {
        private List<string> commands = new List<string>();

        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var s in commands)
            {
                int sum = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsLetter(s[i]) || char.IsDigit(s[i]))
                    {
                        sum++;
                    }
                    else if (s[i] == '\\')
                    {
                        sum += 2;
                    }
                    else
                    {
                        if (i == 0 || i + 1 == s.Length)
                        {
                            sum += 3;
                        }
                        else
                        {
                            sum += 2;
                        }
                    }
                }
                ans += sum - s.Length;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day08\input.txt";
            commands = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
