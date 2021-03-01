using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day04
{
    public class Day4Part2
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
            foreach (var kv in guards)
            {
                guardsShiftLog.Add(kv.Key, new List<char[]>());

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
                    }

                    guardsShiftLog[kv.Key].Add(dayInMinutes);
                }
            }

            Dictionary<int, (int count, int ID)> dp = new Dictionary<int, (int count, int ID)>(); //Key: minute - values: count & ID
            for (int i = 0; i < 60; i++)
            {
                dp.Add(i, (0, 0));
            }

            foreach (var kv in guardsShiftLog) //Loop every guards shift log
            {
                int n = kv.Value.Count;
                for (int i = 0; i < 60; i++) //Loop every minute
                {
                    int sleepCount = 0;
                    for (int j = 0; j < n; j++) //Loop every shift in that specific minute
                    {
                        if (kv.Value[j][i] == '#')
                        {
                            sleepCount++;
                        }
                    }

                    if (sleepCount > dp[i].count)
                    {
                        dp[i] = (sleepCount, kv.Key);
                    }
                }
            }

            int minute = 0, count = 0, id = 0;
            foreach (var kv in dp)
            {
                if (kv.Value.count > count)
                {
                    minute = kv.Key;
                    count = kv.Value.count;                    
                    id = kv.Value.ID;
                }
            }

            int ans = id * minute;

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
