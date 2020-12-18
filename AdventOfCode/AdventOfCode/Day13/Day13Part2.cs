using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13
{
    public class Day13Part2
    {
        public class Bus
        {
            public long Interval; //ID
            public long DepartTime;
            public long TimeStampOffset;

            public long departure;

            public Bus(long interval, long timeStampOffset)
            {
                Interval = interval;
                TimeStampOffset = timeStampOffset;

                while (DepartTime < departure)
                {
                    DepartTime += interval;
                }
            }
        }

        private readonly List<Bus> busses = new List<Bus>();

        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int amountToSync = 2, amountOfBussesInPosition = 0, amountOfSyncedBusses = 0, n = busses.Count;
            long time = busses[0].Interval, previousTime = 0, increment = busses[0].Interval, ans = 0;

            bool isSeen = false;
            while (true)
            {
                foreach (var bus in busses)
                {
                    long result = time + bus.TimeStampOffset;
                    if (result % bus.Interval == 0)
                    {
                        amountOfBussesInPosition++;
                    }
                }

                if (amountOfBussesInPosition == n) 
                {
                    ans = time;
                    break;
                }
                else
                {
                    amountOfBussesInPosition = 0;
                }

                foreach (var bus in busses)
                {
                    long result = time + bus.TimeStampOffset;
                    if (result % bus.Interval == 0)
                    {
                        amountOfSyncedBusses++;

                        if (amountOfSyncedBusses == amountToSync)
                        {
                            amountOfSyncedBusses = 0;

                            if (!isSeen)
                            {
                                isSeen = true;
                                previousTime = time;                                
                            }
                            else
                            {
                                isSeen = false;
                                increment = time - previousTime;
                                amountToSync++;
                            }                            
                        }
                    }
                    else
                    {
                        amountOfSyncedBusses = 0;
                        break;
                    }
                }

                time += increment;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 13\input.txt";
            var fileRead = File.ReadAllLines(path);

            long departure = long.Parse(fileRead[0]);
            var split = fileRead[1].Split(',');
            int n = split.Length;
            for (int i = 0; i < n; i++)
            {
                string s = split[i];
                if (s[0] != 'x')
                {
                    long interval = long.Parse(s);
                    var bus = new Bus(interval, i)
                    {
                        departure = departure
                    };
                    busses.Add(bus);
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day13();
        }
    }
}
