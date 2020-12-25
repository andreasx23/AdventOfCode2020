using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public class Day4Part2
    {
        private List<Dictionary<string, string>> listMap = new List<Dictionary<string, string>>();

        private void Day4()
        {
            int ans = 0;            
            foreach (var map in listMap)
            {
                bool byr = false, iyr = false, eyr = false, hgt = false, hcl = false, ecl = false, pid = false, cid = false;
                if (map.ContainsKey("byr"))
                {
                    int num = int.Parse(map["byr"]);
                    if (num >= 1920 && num <= 2002) 
                    {
                        //Console.WriteLine(num);
                        byr = true;
                    }
                }

                if (map.ContainsKey("iyr"))
                {
                    int num = int.Parse(map["iyr"]);
                    if (num >= 2010 && num <= 2020)
                    {
                        //Console.WriteLine(num);
                        iyr = true;
                    }
                }

                if (map.ContainsKey("eyr"))
                {
                    int num = int.Parse(map["eyr"]);
                    if (num >= 2020 && num <= 2030)
                    {
                        //Console.WriteLine(num);
                        eyr = true;
                    }
                }

                if (map.ContainsKey("hgt"))
                {
                    string value = map["hgt"];
                    if (value.Contains("in"))
                    {
                        int num = int.Parse(value.Split('i')[0]);
                        if (num >= 59 && num <= 76)
                        {
                            //Console.WriteLine(value);
                            hgt = true;
                        }
                    }
                    else if (value.Contains("cm"))
                    {
                        int num = int.Parse(value.Split('c')[0]);
                        if (num >= 150 && num <= 193)
                        {
                            //Console.WriteLine(value);
                            hgt = true;
                        }
                    }                    
                }

                if (map.ContainsKey("hcl"))
                {
                    string value = map["hcl"];
                    if (value[0] == '#')
                    {
                        int count = 0, n = value.Length;
                        for (int i = 1; i < n; i++)
                        {
                            var c = value[i];
                            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };
                            if (chars.Contains(c) || char.IsNumber(c))
                            {
                                count++;
                            }
                        }
                        
                        if (count == 6)
                        {
                            //Console.WriteLine(value);
                            hcl = true;
                        }
                    }
                }

                if (map.ContainsKey("ecl"))
                {
                    string[] matches = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    string value = map["ecl"];
                    if (matches.Contains(value))
                    {
                        //Console.WriteLine(value);
                        ecl = true;
                    }
                }

                if (map.ContainsKey("pid"))
                {
                    string value = map["pid"];
                    if (value.Length == 9)
                    {
                        int count = 0;
                        for (int i = 0; i < 9; i++)
                        {
                            char c = value[i];
                            if (char.IsNumber(c))
                            {
                                count++;
                            }
                        }

                        if (count == 9)
                        {
                            //Console.WriteLine(value);
                            pid = true;
                        }
                    }
                }

                if (byr && iyr && eyr && hgt && hcl && ecl && pid)
                {
                    ans++;
                }
            }

            Console.WriteLine("Answer: " + ans);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 4\input.txt";
            var input = File.ReadAllLines(path);

            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (var s in input)
            {
                if (string.IsNullOrEmpty(s))
                {
                    listMap.Add(map);
                    map = new Dictionary<string, string>();
                    continue;
                }

                var split = s.Split(' ');
                var n = split.Length;
                for (int i = 0; i < n; i++)
                {
                    var keyValue = split[i].Split(':');
                    string key = keyValue[0].Trim(), value = keyValue[1].Trim();

                    if (!map.ContainsKey(key))
                    {
                        map.Add(key, value);
                    }
                }
            }
            listMap.Add(map);
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
