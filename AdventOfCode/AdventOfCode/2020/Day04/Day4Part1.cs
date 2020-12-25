using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public class Day4Part1
    {
        private List<Dictionary<string, string>> listMap = new List<Dictionary<string, string>>();

        private void Day4()
        {
            int ans = 0;
            foreach (var map in listMap)
            {
                if (map.ContainsKey("byr") &&
                    map.ContainsKey("iyr") &&
                    map.ContainsKey("eyr") &&
                    map.ContainsKey("hgt") &&
                    map.ContainsKey("hcl") &&
                    map.ContainsKey("ecl") &&
                    map.ContainsKey("pid")
                    //&& map.ContainsKey("cid")
                    )
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
