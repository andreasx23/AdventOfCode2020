using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
    public class Day2Part1
    {
        private List<string> input = new List<string>();

        public void Day2()
        {
            int ans = 0;
            foreach (var item in input)
            {
                var split = item.Split(':');

                string[] policy = split[0].Split(' ');
                string[] nums = policy[0].Split('-');
                int min = int.Parse(nums[0]), max = int.Parse(nums[1]);
                char letter = char.Parse(policy[1]);
                string password = split[1].Trim();
                int count = 0;
                foreach (var c in password)
                {
                    if (letter == c)
                    {
                        count++;
                    }
                }

                if (count >= min && count <= max)
                {
                    ans++;
                }
            }

            Console.WriteLine(ans);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 2\input.txt";
            input = File.ReadAllLines(path).ToList();            
        }

        public void TestCase()
        {
            ReadData();
            Console.WriteLine("Expected: 2020");
            Day2();
        }
    }
}
