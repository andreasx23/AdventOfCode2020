using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day12
{
    public class Day12Part2
    {
        private string input = string.Empty;

        /* I didn't make this but it's really cool
         * https://www.reddit.com/r/adventofcode/comments/3wh73d/day_12_solutions/cxw7z9h?utm_source=share&utm_medium=web2x&context=3
         */
        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            dynamic obj = JsonConvert.DeserializeObject(input);
            long ans = GetSum(obj, "red");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private long GetSum(JObject o, string avoid = null)
        {
            bool shouldAvoid = o.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);

            return shouldAvoid ? 0 : o.Properties().Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }

        private long GetSum(JArray arr, string avoid) => arr.Sum((dynamic a) => (long)GetSum(a, avoid));

        private long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long)val.Value : 0;

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day12\input.txt";
            input = File.ReadAllLines(path)[0].ToLower();
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
