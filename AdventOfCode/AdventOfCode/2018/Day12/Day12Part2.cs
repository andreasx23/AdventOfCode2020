using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day12
{
    public class Day12Part2
    {
        private StringBuilder currentGeneration = new StringBuilder();
        private readonly Dictionary<string, string> rules = new Dictionary<string, string>();

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long currentLeft = 0;
            long score = 0, prevScore = 0;
            long prevDifference = 0;
            long maxGens = 50000000000;
            for (long generation = 1; generation <= maxGens; generation++)
            {
                var temp = currentGeneration.ToString();
                StringBuilder nextGeneration = new StringBuilder();

                int n = currentGeneration.Length;
                for (int pos = -2; pos < n + 2; pos++)
                {
                    string state = string.Empty;
                    int distFromEnd = temp.Length - pos;
                    if (pos <= 1)
                    {
                        state = new string('.', 2 - pos) + temp.Substring(0, 3 + pos);
                    }
                    else if (distFromEnd <= 2)
                    {
                        state = temp.Substring(pos - 2, distFromEnd + 2) + new string('.', 3 - distFromEnd);
                    }
                    else
                    {
                        state = temp.Substring(pos - 2, 5);
                    }

                    if (rules.TryGetValue(state, out string value))
                    {
                        nextGeneration.Append(value);
                    }
                    else
                    {
                        nextGeneration.Append(".");
                    }
                }
                currentGeneration = nextGeneration;
                currentLeft -= 2;

                score = 0;
                for (int pos = 0; pos < currentGeneration.Length; pos++)
                {
                    score += currentGeneration.ToString()[pos].ToString() == "." ? 0 : pos + currentLeft;
                }

                long difference = score - prevScore;
                if (difference == prevDifference)
                {
                    score += (maxGens - generation) * prevDifference;
                    break;
                }

                prevDifference = difference;
                prevScore = score;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {score} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day12\input.txt";
            var lines = File.ReadAllLines(path);
            currentGeneration.Append(lines[0].Replace("initial state: ", ""));

            for (int i = 2; i < lines.Length; i++)
            {
                var split = lines[i].Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(split[0], split[1]);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
