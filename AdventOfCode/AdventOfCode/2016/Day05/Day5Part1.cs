using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day05
{
    public class Day5Part1
    {
        private const string input = "ojvtpuvg";

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = string.Empty;
            int index = 0;
            while (ans.Length < 8)
            {
                while (true)
                {
                    string current = input + index.ToString();
                    string hash = MD5Hash.CreateMD5(current);
                    index++;

                    if (index % 10000 == 0)
                    {
                        Console.WriteLine(index);
                    }

                    if (hash.Take(5).All(c => c == '0'))
                    {
                        ans += hash[5];
                        Console.WriteLine(ans);
                        break;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day04\input.txt";
            //var lines = File.ReadAllLines(path);
        }

        public void TestCase()
        {
            ReadData();
            Day5();
        }
    }
}
