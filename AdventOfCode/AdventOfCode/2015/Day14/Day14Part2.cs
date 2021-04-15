using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day14
{
    public class Day14Part2
    {
        private readonly List<Reindeer> reindeers = new List<Reindeer>();
        private readonly int time = 2503;

        private void Day14()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < time; i++)
            {
                foreach (var r in reindeers)
                {
                    if (r.IsResting)
                    {
                        r.RestCounter++;

                        if (r.RestCounter == r.TotalRestTime)
                        {
                            r.RestCounter = 0;
                            r.IsResting = false;
                        }
                    }
                    else
                    {
                        r.TurnCounter++;
                        r.TotalDistance += r.KMSeconds;

                        if (r.TurnCounter == r.TotalTurnsBeforeRest)
                        {
                            r.TurnCounter = 0;
                            r.IsResting = true;
                        }
                    }
                }

                int max = reindeers.Max(r => r.TotalDistance);
                var leads = reindeers.Where(r => r.TotalDistance == max);
                foreach (var r in leads)
                {
                    r.Points++;
                }
            }

            int ans = reindeers.Max(r => r.Points);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day14\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                Reindeer reindeer = new Reindeer();
                var index = s.IndexOf(' ');
                var id = s.Substring(0, index);
                reindeer.Id = id;
                var remaining = s.Substring(index + 1).Replace("can fly ", "");
                var split = remaining.Split(',').Select(_ => _.Trim()).ToArray();

                int kms = 0, duration = 0;
                string temp = "";
                bool isKms = true;
                foreach (var c in split[0])
                {
                    if (char.IsDigit(c))
                    {
                        temp += c;
                    }
                    else if (!string.IsNullOrEmpty(temp))
                    {
                        if (isKms)
                        {
                            kms = int.Parse(temp);
                            isKms = false;
                        }
                        else
                        {
                            duration = int.Parse(temp);
                        }
                        temp = "";
                    }
                    else
                    {
                        temp = "";
                    }
                }

                int rest = 0;
                temp = "";
                foreach (var c in split[1])
                {
                    if (char.IsDigit(c))
                    {
                        temp += c;
                    }
                    else if (!string.IsNullOrEmpty(temp))
                    {
                        rest = int.Parse(temp);
                        break;
                    }
                    else
                    {
                        temp = "";
                    }
                }

                reindeer.KMSeconds = kms;
                reindeer.TotalTurnsBeforeRest = duration;
                reindeer.TotalRestTime = rest;

                reindeers.Add(reindeer);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day14();
        }
    }
}
