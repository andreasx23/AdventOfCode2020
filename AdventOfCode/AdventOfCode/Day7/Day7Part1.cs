using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
{
   public class Day7Part1
    {
        private Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        private HashSet<string> canContain = new HashSet<string>();
        private const string colour = "shiny gold";

        //Answer 316
        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int ans = canContain.Count;

            bool isFinished = false;
            while (!isFinished)
            {
                int tempAns = ans;
                foreach (var kv in map)
                {
                    foreach (var bag in kv.Value)
                    {
                        if (canContain.Contains(kv.Key))
                        {
                            break;
                        }

                        if (canContain.Contains(bag))
                        {
                            ans++;
                            canContain.Add(kv.Key);
                        }
                    }
                }

                if (ans == tempAns)
                {
                    isFinished = true;
                }
            }
            watch.Stop();

            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms to complete");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 7\input.txt";
            var input = File.ReadAllLines(path).ToList();

            foreach (var s in input)
            {
                var split = s.Split(new string[] { "contain" }, StringSplitOptions.None);
                string bag = RemoveBagTag(split[0]);
                map.Add(bag, new List<string>());

                for (int i = 1; i < split.Length; i++)
                {
                    string[] contents = split[1].Split(',');

                    foreach (var _content in contents)
                    {
                        var content = _content.Trim();

                        int fromIndex = 0;
                        while (char.IsDigit(content[fromIndex]) || char.IsWhiteSpace(content[fromIndex]))
                        {
                            fromIndex++;
                        }
                    
                        content = content.Substring(fromIndex);
                        content = RemoveBagTag(content);
                        if (content.Contains(colour))
                        {
                            canContain.Add(bag);
                        }
                        else
                        {
                            map[bag].Add(content);
                        }
                    }
                }
            }
        }

        //Removes "Bag" or "Bags" from string
        private string RemoveBagTag(string bag)
        {
            StringBuilder sb = new StringBuilder();
            int spaceCounter = 0;
            foreach (var c in bag)
            {
                if (char.IsWhiteSpace(c))
                {
                    spaceCounter++;

                    if (spaceCounter == 2)
                    {
                        break;
                    }
                }

                sb.Append(c);
            }
            return sb.ToString();
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
