using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day08
{
    public class Day8Part2
    {
        private readonly List<(List<string> left, List<string> right)> _parts = new List<(List<string> left, List<string> right)>();

        private void Day8()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int ans = 0;
            foreach (var (left, right) in _parts)
            {
                //Get 1, 4, 7 and 8
                string d1 = left.Single(x => x.Length == 2);
                string d4 = left.Single(x => x.Length == 4);
                string d7 = left.Single(x => x.Length == 3);
                string d8 = left.Single(x => x.Length == 7);

                //Get 2, 3 and 5
                List<string> l5 = left.Where(x => x.Length == 5).ToList();

                //Get 0, 6 and 9
                List<string> l6 = left.Where(x => x.Length == 6).ToList();

                //3 contains all of 1
                string d3 = l5.Single(x => d1.All(x.Contains));
                l5.Remove(d3);

                //9 contains all of 3
                string d9 = l6.Single(x => d3.All(x.Contains));
                l6.Remove(d9);

                //0 contains all of 1
                string d0 = l6.Single(x => d1.All(x.Contains));
                l6.Remove(d0);
                string d6 = l6.Single();

                //6 contains all of 5
                string d5 = l5.Single(x => x.All(d6.Contains));
                l5.Remove(d5);
                string d2 = l5.Single();

                //Order the numbers alphabetically
                List<string> numbers = new List<string>
                {
                    string.Join(string.Empty, d0.OrderBy(x => x)),
                    string.Join(string.Empty, d1.OrderBy(x => x)),
                    string.Join(string.Empty, d2.OrderBy(x => x)),
                    string.Join(string.Empty, d3.OrderBy(x => x)),
                    string.Join(string.Empty, d4.OrderBy(x => x)),
                    string.Join(string.Empty, d5.OrderBy(x => x)),
                    string.Join(string.Empty, d6.OrderBy(x => x)),
                    string.Join(string.Empty, d7.OrderBy(x => x)),
                    string.Join(string.Empty, d8.OrderBy(x => x)),
                    string.Join(string.Empty, d9.OrderBy(x => x))
                };

                string output = string.Empty;
                foreach (var number in right)
                {
                    string n = string.Join("", number.OrderBy(x => x));
                    output += numbers.IndexOf(n);
                }
                ans += int.Parse(output);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day08\input.txt";
            var input = File.ReadAllLines(path).Select(s => s.Split('|').Select(s => s.Split(' ').ToList()).ToList()).ToList();
            foreach (var item in input)
                _parts.Add((item.First().Where(s => !string.IsNullOrEmpty(s)).ToList(), item.Last().Where(s => !string.IsNullOrEmpty(s)).ToList()));
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
