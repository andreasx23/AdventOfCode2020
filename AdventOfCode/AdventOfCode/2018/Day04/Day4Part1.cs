using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day04
{
    public class Day4Part1
    {
        class Shift
        {
            public DateTime beginShift; //year-month-day hour:minute
            public List<DateTime> fallsAsleep;
            public List<DateTime> wakesUp;

            public Shift()
            {
                fallsAsleep = new List<DateTime>();
                wakesUp = new List<DateTime>();
            }
        }

        private readonly Dictionary<int, List<Shift>> guards = new Dictionary<int, List<Shift>>(); //Key: Guard ID - Value: List of shifts for that specific guard

        private void Day4()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Dictionary<int, List<char[]>> guardsShiftLog = new Dictionary<int, List<char[]>>();
            (int id, int totalMinAsleep) mostSleepyGuard = (-1, -1);
            foreach (var kv in guards)
            {
                int id = kv.Key;

                guardsShiftLog.Add(id, new List<char[]>());

                int totalAsleep = 0;
                foreach (var currentShift in kv.Value)
                {
                    char[] dayInMinutes = new char[60];
                    for (int i = 0; i < currentShift.fallsAsleep.Count; i++)
                    {
                        var fellAsleep = currentShift.fallsAsleep[i];
                        var wakesUp = currentShift.wakesUp[i];

                        for (int j = fellAsleep.Minute; j < wakesUp.Minute; j++)
                        {
                            dayInMinutes[j] = '#';
                        }
                        totalAsleep += (int)wakesUp.Subtract(fellAsleep).TotalMinutes;
                    }

                    guardsShiftLog[id].Add(dayInMinutes);
                }

                if (totalAsleep > mostSleepyGuard.totalMinAsleep)
                {
                    mostSleepyGuard = (id, totalAsleep);
                }
            }

            var mostSleepyGuardShiftLog = guardsShiftLog[mostSleepyGuard.id];
            int bestMinute = -1, maxSleepCount = -1, n = mostSleepyGuardShiftLog.Count;
            for (int i = 0; i < 60; i++)
            {
                int sleepCount = 0;
                for (int j = 0; j < n; j++)
                {
                    if (mostSleepyGuardShiftLog[j][i] == '#')
                    {
                        sleepCount++;
                    }
                }

                if (sleepCount > maxSleepCount)
                {
                    maxSleepCount = sleepCount;
                    bestMinute = i;
                }
            }

            int ans = mostSleepyGuard.id * bestMinute;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2018\Day04\input.txt";
            var lines = File.ReadAllLines(path).OrderBy(s => s);

            Shift currentShift = null;
            var currentId = -1;
            foreach (var s in lines)
            {
                if (s.Contains("Guard")) //New guard
                {
                    if (currentShift != null)
                    {
                        guards[currentId].Add(currentShift);
                    }

                    currentShift = new Shift();

                    var split = s.Split(new string[] { "Guard" }, StringSplitOptions.RemoveEmptyEntries);
                    var beginShift = DateTime.Parse(split[0].Trim().Substring(1, split[0].Length - 3));

                    currentId = int.Parse(split[1].Trim().Substring(1, split[1].Length - 15));
                    currentShift.beginShift = beginShift;

                    if (!guards.ContainsKey(currentId))
                    {
                        guards.Add(currentId, new List<Shift>());
                    }
                }
                else if (s.Contains("falls asleep"))
                {
                    var split = s.Split(new string[] { "falls asleep" }, StringSplitOptions.RemoveEmptyEntries);
                    var asleep = DateTime.Parse(split[0].Trim().Substring(1, split[0].Length - 3));
                    currentShift.fallsAsleep.Add(asleep);
                }
                else if (s.Contains("wakes up"))
                {
                    var split = s.Split(new string[] { "wakes up" }, StringSplitOptions.RemoveEmptyEntries);
                    var wakesUp = DateTime.Parse(split[0].Trim().Substring(1, split[0].Length - 3));
                    currentShift.wakesUp.Add(wakesUp);
                }
            }
            guards[currentId].Add(currentShift);
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
