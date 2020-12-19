using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
{
    public class Day6Part1
    {
        private List<string> input = new List<string>();

        private void Day6()
        {
            Dictionary<char, int> groupChars = new Dictionary<char, int>();
            int ans = 0;
            foreach (var s in input)
            {
                if (!string.IsNullOrEmpty(s)) 
                {
                    foreach (var c in s)
                    {
                        if (!groupChars.ContainsKey(c))
                        {
                            groupChars.Add(c, 1);
                            ans++;
                        }
                    }
                }
                else
                {
                    groupChars = new Dictionary<char, int>();
                }
            }
            Console.WriteLine(ans);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 6\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
