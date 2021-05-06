using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day08
{
    public class Day8Part1
    {
        private List<string> commands = new List<string>();

        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var s in commands)
            {
                int inMemory = 0;
                for (int i = 1; i < s.Length - 1; i++)
                {
                    if (s[i] == '\\')
                    {
                        i++;

                        if (s[i] == 'x')
                        {
                            i += 2;
                        }
                    }
                    inMemory++;
                }

                ans += s.Length - inMemory;
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
