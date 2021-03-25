using AdventOfCode._2016.Day05;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day14
{
    public class Day14Part1
    {
        private static readonly bool isSample = false;
        private readonly string input = (isSample) ? "abc" : "ahsbgdzn";

        private void Day14()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<(string triple, int i)> founds = new List<(string triple, int i)>();
            int sum = 0, index = 0, ans = -1;
            bool isFinished = false;
            while (!isFinished)
            {
                string hash = GetMD5($"{input}{index}");

                var triple = Regex.Match(hash, "([a-z0-9\\d])\\1\\1");
                if (triple.Success)
                {
                    string firstTriple = triple.Groups[0].Value;
                    founds.Add((firstTriple, index));
                }

                var fives = Regex.Match(hash, "([a-z0-9\\d])\\1\\1\\1\\1");
                if (fives.Success)
                {
                    string firstFives = fives.Groups[0].Value;
                    string lookup = new string(firstFives.Take(3).ToArray());

                    var matches = founds.Where(f => f.triple.Equals(lookup) && index - f.i <= 1000 && index != f.i).ToList();
                    foreach (var match in matches)
                    {
                        sum++;

                        if (sum >= 64)
                        {
                            ans = match.i;
                            isFinished = true;
                            break;
                        }

                        founds.Remove(match);
                    }
                }

                var outdated = founds.Where(f => index - f.i > 1000).ToList();
                foreach (var key in outdated)
                {
                    founds.Remove(key);
                }

                index++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string GetMD5(string input)
        {
            return MD5Hash.CreateMD5(input).ToLower();
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day12\input.txt";
            //input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day14();
        }
    }
}
