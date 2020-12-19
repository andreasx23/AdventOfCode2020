using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day7
{
    public class Day7Part2
    {
        private Dictionary<string, Dictionary<string, int>> map = new Dictionary<string, Dictionary<string, int>>();
        private const string colour = "shiny gold";

        /* https://adventofcode.com/2020/day/7#part2
         * shiny gold bags contain 2 muted aqua bags, 3 bright salmon bags, 4 striped violet bags, 2 posh brown bags.
         * ---------------
         * muted aqua bags contain 3 mirrored tomato bags, 2 dim bronze bags, 1 pale purple bag, 5 mirrored aqua bags. 
         * bright salmon bags contain 2 mirrored chartreuse bags, 3 pale brown bags, 5 faded plum bags, 1 striped cyan bag.
         * striped violet bags contain 1 dim bronze bag, 2 faded plum bags.
         * posh brown bags contain 2 vibrant magenta bags, 1 muted salmon bag.
         * ---------------
         * shiny gold bags contain 2 dark red bags.
         * dark red bags contain 2 dark orange bags.
         * dark orange bags contain 2 dark yellow bags.
         * dark yellow bags contain 2 dark green bags.
         * dark green bags contain 2 dark blue bags.
         * dark blue bags contain 2 dark violet bags.
         * dark violet bags contain no other bags.
         * 
         * i.e. 1 shiny = 2 red + 2 red * 2 orange + 2 red * 2 orange * 2 yellow + 
         *        2 red * 2 orange * 2 yellow * 2 green + 2 red * 2 orange * 2 yellow * 2 green * 2 blue+
         *        2 red * 2 orange * 2 yellow * 2 green * 2 blue * 2 dark violet + 
         *        2 red * 2 orange * 2 yellow * 2 green * 2 blue * 2 dark violet * 0 dark violet
         *        = 2 + 2**2 + 2**3 + 2 ** 4 + 2 ** 5 + 2 ** 6
         *        = 126
         * 
         * Answer: 126
         * ---------------
         * Returns (wrong answer): 11121
         */
        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Queue<Dictionary<string, int>> tree = new Queue<Dictionary<string, int>>();
            Queue<Queue<Dictionary<string, int>>> queues = new Queue<Queue<Dictionary<string, int>>>();

            tree.Enqueue(map[colour]); //Base case
            var queueToAdd = new Queue<Dictionary<string, int>>(); //Base case
            queueToAdd.Enqueue(map[colour]);
            queues.Enqueue(queueToAdd);

            while (tree.Count > 0) //Populate tree
            {
                var currentMap = tree.Dequeue();

                queueToAdd = new Queue<Dictionary<string, int>>();
                foreach (var kv_currentBag in currentMap)
                {
                    if (map.ContainsKey(kv_currentBag.Key))
                    {
                        tree.Enqueue(map[kv_currentBag.Key]);
                        queueToAdd.Enqueue(map[kv_currentBag.Key]);
                    }
                }
                queues.Enqueue(queueToAdd);
            }

            int ans = 0, depth = 1, bagCount = 0, timeToReset = 0;
            while (queues.Count > 0)
            {
                var queue = queues.Dequeue();

                if (timeToReset == 0)
                {
                    timeToReset = queue.Count;
                }

                while (queue.Count > 0)
                {
                    var currentMap = queue.Dequeue();

                    foreach (var kv_currentBag in currentMap)
                    {
                        if (map.ContainsKey(kv_currentBag.Key))
                        {
                            bagCount += kv_currentBag.Value;
                            ans += (int)Math.Pow(kv_currentBag.Value, depth);
                        }
                    }
                }

                timeToReset--;
                if (timeToReset == 0)
                {
                    depth++;
                }
            }

            watch.Stop();
            Console.WriteLine("Expected answer: 11310");
            Console.WriteLine("Amount of bags: " + bagCount);
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
            Console.WriteLine();
        }

        //Returns 11310 (Correct answer)
        private void Day7Steal()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = MustContain(colour);

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private int MustContain(string colour)
        {
            int sum = 0;
            if (map.ContainsKey(colour))
            {
                sum = map[colour].Sum(kvp => kvp.Value * (1 + MustContain(kvp.Key)));
            }
            return sum;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 7\input.txt";
            var input = File.ReadAllLines(path).ToList();

            foreach (var s in input)
            {
                var split = s.Split(new string[] { "contain" }, StringSplitOptions.None);
                string bag = RemoveBagTag(split[0]);
                map.Add(bag, new Dictionary<string, int>());

                for (int i = 1; i < split.Length; i++)
                {
                    string[] contents = split[1].Split(',');

                    foreach (var _content in contents)
                    {
                        var content = _content.Trim();

                        int digits = 0;
                        while (char.IsDigit(content[digits]))
                        {
                            digits++;
                        }

                        int amount = (digits > 0) ? int.Parse(content.Substring(0, digits)) : 0;

                        content = content.Substring(digits, content.Length - digits).Trim();
                        content = RemoveBagTag(content);
                        map[bag].Add(content, amount);
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
            Day7Steal();
        }
    }
}
