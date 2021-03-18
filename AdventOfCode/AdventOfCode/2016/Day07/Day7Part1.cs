using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day07
{
    public class Day7Part1
    {
        private readonly Dictionary<List<string>, List<string>> map = new Dictionary<List<string>, List<string>>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            foreach (var kv in map)
            {
                bool hypernetContainsPalindrome = false;
                foreach (var hypernet in kv.Value)
                {
                    for (int i = 0; i < hypernet.Length - 3; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < 4; j++)
                        {
                            sb.Append(hypernet[j + i]);
                        }

                        string temp = sb.ToString();
                        if (!temp.All(t => t == temp[0]) && IsPalindrome(temp))
                        {
                            hypernetContainsPalindrome = true;
                            break;
                        }
                    }

                    if (hypernetContainsPalindrome)
                    {
                        break;
                    }
                }
                
                if (hypernetContainsPalindrome)
                {
                    continue;
                }
                
                bool ipv7ContainsPalindrome = false;
                foreach (var ipv7 in kv.Key)
                {
                    for (int i = 0; i < ipv7.Length - 3; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < 4; j++)
                        {
                            sb.Append(ipv7[j + i]);
                        }

                        string temp = sb.ToString();
                        if (!temp.All(t => t == temp[0]) && IsPalindrome(temp))
                        {
                            ipv7ContainsPalindrome = true;                            
                            break;
                        }
                    }

                    if (ipv7ContainsPalindrome)
                    {
                        break;
                    }
                }

                if (ipv7ContainsPalindrome)
                {
                    ans++;
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsPalindrome(string validate)
        {
            int left = 0, right = validate.Length - 1;

            while (left <= right)
            {
                if (validate[left] != validate[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day07\input.txt";
            var lines = File.ReadAllLines(path).ToList();
            
            foreach (var s in lines)
            {
                List<string> ipv7s = new List<string>(), hypernets = new List<string>();
                StringBuilder ipv7 = new StringBuilder(), hypernet = new StringBuilder();

                bool isHypernet = false;
                foreach (var c in s)
                {
                    if (c == '[')
                    {
                        isHypernet = true;
                        ipv7s.Add(ipv7.ToString());
                        ipv7 = new StringBuilder();                        
                        continue;
                    }
                    else if (c == ']')
                    {
                        isHypernet = false;
                        hypernets.Add(hypernet.ToString());
                        hypernet = new StringBuilder();
                        continue;
                    }

                    if (isHypernet)
                    {
                        hypernet.Append(c);
                    }
                    else
                    {
                        ipv7.Append(c);
                    }
                }
                ipv7s.Add(ipv7.ToString());
                
                map.Add(ipv7s, hypernets);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
