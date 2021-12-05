using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day03
{
    public class Day3Part1
    {
        private List<string> _bits = new List<string>();

        private void Day3()
        {
            Stopwatch watch = Stopwatch.StartNew();

            string gamma = string.Empty, epision = string.Empty;
            int n = _bits.Count;
            for (int i = 0; i < _bits.First().Length; i++)
            {
                int zero = 0, one = 0;
                for (int j = 0; j < n; j++)
                {
                    if (_bits[j][i] == '0')
                        zero++;
                    else
                        one++;
                }
                if (zero > one)
                {
                    gamma += "0";
                    epision += "1";
                }
                else
                {
                    gamma += "1";
                    epision += "0";
                }
            }

            int gammaRate = Convert.ToInt32(gamma, 2);
            int epsilonRate = Convert.ToInt32(epision, 2);
            int ans = gammaRate * epsilonRate;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
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
