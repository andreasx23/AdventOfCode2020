using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
    public class Day2Part2
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
                int min = int.Parse(nums[0]) - 1, max = int.Parse(nums[1]) - 1;
                char letter = char.Parse(policy[1]);
                string password = split[1].Trim();

                bool firstCase = password[min] == letter, secondCase = password[max] == letter;
                
                if (firstCase && !secondCase || !firstCase && secondCase)
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
            Day2();
        }
    }
}
