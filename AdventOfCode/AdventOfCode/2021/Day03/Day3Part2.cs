using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day03
{
    public class Day3Part2
    {
        private List<string> _bits = new List<string>();

        private void Day3()
        {
            Stopwatch watch = Stopwatch.StartNew();

            string oxygen = Generator(_bits, 0, true);
            string co2Scrubber = Generator(_bits, 0, false);
            int ans = Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2Scrubber, 2);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private string Generator(List<string> bits, int index, bool findMostCommon)
        {
            string value = FindCommonAtIndex(bits, index, findMostCommon);
            List<string> filter = bits.Where(s => s[index].ToString() == value).ToList();
            return filter.Count == 1 ? filter.First() : Generator(filter, index + 1, findMostCommon);
        }

        private string FindCommonAtIndex(List<string> bits, int index, bool findMostCommon)
        {
            int zero = 0, one = 0, n = bits.Count;
            for (int j = 0; j < n; j++)
            {
                if (bits[j][index] == '0')
                    zero++;
                else
                    one++;
            }
            if (findMostCommon)
                return one >= zero ? "1" : "0";
            else
                return zero <= one ? "0" : "1";
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day03\input.txt";
            _bits = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day3();
        }
    }
}
