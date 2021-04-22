using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day11
{
    public class Day11Part2
    {
        private readonly string input = "vzbxxyzz";

        private void Day11()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var temp = input.ToCharArray();
            int n = temp.Length;
            string ans = string.Empty;
            while (true)
            {
                int index = n - 1;
                if (temp[index] == 'z')
                {
                    while (index >= 0 && temp[index] == 'z')
                    {
                        index--;
                    }

                    if (index == -1)
                    {
                        Console.WriteLine("Error - no valid password found");
                        break;
                    }
                    else
                    {
                        temp[index] = GenerateNextChar(temp[index]);
                        index++;
                        while (index < n)
                        {
                            temp[index] = 'a';
                            index++;
                        }
                    }
                }
                else
                {
                    temp[index] = GenerateNextChar(temp.Last());
                }

                if (IsValidPassword(temp))
                {
                    ans = new string(temp);
                    break;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private char GenerateNextChar(char current)
        {
            var asciiValue = current - 'a';
            return Convert.ToChar('a' + asciiValue + 1);
        }

        private bool IsValidPassword(char[] input)
        {
            bool isFirstConditionValid = false;
            for (int i = 0; i < input.Length - 2; i++)
            {
                char left = input[i], middle = input[i + 1], right = input[i + 2];
                if (right - middle == 1 && middle - left == 1)
                {
                    isFirstConditionValid = true;
                    break;
                }
            }
            if (!isFirstConditionValid) return false;

            char[] isSecondConditionValid = new char[] { 'i', 'o', 'l' };
            if (input.Any(isSecondConditionValid.Contains)) return false;

            int isThirdConditionValid = 0;
            for (int i = 1; i < input.Length; i++)
            {
                char left = input[i - 1], current = input[i];
                if (left == current)
                {
                    isThirdConditionValid++;
                    i++;

                    if (isThirdConditionValid == 2) break;
                }
            }
            if (isThirdConditionValid != 2) return false;

            return true;
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day12\input.txt";
            //input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day11();
        }
    }
}
