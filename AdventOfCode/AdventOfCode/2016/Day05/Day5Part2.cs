using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day05
{
    public class Day5Part2
    {
        private const string input = "ojvtpuvg";

        private void Day5()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            char[] ans = new char[8];
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = '#';
            }

            int index = 0;
            while (ans.Any(c => c == '#'))
            {
                while (true)
                {
                    string current = input + index.ToString();
                    string hash = MD5Hash.CreateMD5(current);
                    index++;

                    if (index % 25000 == 0)
                    {
                        Console.WriteLine(index);
                    }

                    if (hash.Take(5).All(c => c == '0'))
                    {
                        int pos = -1;
                        if (char.IsDigit(hash[5]))
                        {
                            pos = int.Parse(hash[5].ToString());
                        }
                        else
                        {
                            continue;
                        }
                        
                        if (pos >= 8)
                        {
                            continue;
                        }

                        char value = hash[6];
                        if (ans[pos] == '#')
                        {
                            ans[pos] = value;
                            Console.WriteLine(new string(ans));
                            break;
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {new string(ans)} took {watch.ElapsedMilliseconds} ms");
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
