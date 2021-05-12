using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day16
{
    public class Day16Part1
    {
        private List<string> commands = new List<string>();

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = string.Empty;
            for (char i = 'a'; i <= 'p'; i++)
            {
                ans += i;
            }

            foreach (var c in commands)
            {
                var temp = c.Substring(1);
                if (c.First() == 's')
                {
                    ans = Spin(ans, int.Parse(temp));
                }
                else if (c.First() == 'x')
                {
                    var split = temp.Split('/').Select(int.Parse);
                    ans = Exchange(ans, split.First(), split.Last());
                }
                else
                {
                    var split = temp.Split('/').Select(char.Parse);
                    ans = Partner(ans, split.First(), split.Last());
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string Spin(string input, int times)
        {
            var array = input.ToCharArray();
            int n = array.Length;
            for (int i = 0; i < (times % n); i++)
            {
                var temp = new char[n];
                for (int j = 0; j < n; j++)
                {
                    temp[(j + 1) % n] = array[j];
                }
                array = temp;
            }
            return new string(array);
        }

        private string Exchange(string input, int indexA, int indexB)
        {
            var array = input.ToCharArray();
            var temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
            return new string(array);
        }

        private string Partner(string input, char a, char b)
        {
            var indexA = input.IndexOf(a);
            var indexB = input.IndexOf(b);
            return Exchange(input, indexA, indexB);
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day16\input.txt";
            commands = File.ReadAllLines(path).First().Split(',').ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
