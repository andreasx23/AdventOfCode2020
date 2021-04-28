using AdventOfCode._2016.Day05;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day04
{
    public class Day4Part2
    {
        private readonly string input = "yzbqklnj";

        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            for (int i = 1; ; i++)
            {
                var hash = GenerateMD5(input + i.ToString());

                if (hash.Substring(0, 6).All(c => c == '0'))
                {
                    ans = Math.Min(ans, i);
                    break;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string GenerateMD5(string input)
        {
            return MD5Hash.CreateMD5(input);
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day05\input.txt";
            //inputs = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
