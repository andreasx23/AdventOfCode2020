using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
{
    public class Day6Part2
    {
        private List<string> input = new List<string>();

        private void Day6()
        {
            List<int[]> groupAnswers = new List<int[]>();
            int ans = 0;
            foreach (var s in input)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    int[] myAnswers = new int[26];
                    foreach (var c in s)
                    {
                        myAnswers[c - 'a']++;
                    }
                    groupAnswers.Add(myAnswers);
                }
                else
                {
                    ans += Calc(groupAnswers);
                    groupAnswers = new List<int[]>();
                }
            }

            if (groupAnswers.Count > 0)
            {
                ans += Calc(groupAnswers);
            }

            Console.WriteLine(ans);
        }

        private int Calc(List<int[]> groupAnswers)
        {
            int ans = 0, n = groupAnswers.Count;
            for (int i = 0; i < 26; i++)
            {
                bool[] answers = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    if (groupAnswers[j][i] > 0)
                    {
                        answers[j] = true;
                    }
                }

                if (answers.All(a => a))
                {
                    ans++;
                }
            }

            return ans;
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
