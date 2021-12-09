using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day06
{
    public class Day6Part1
    {
        private class Fish
        {
            public int CurrentTimer { get; set; }
            public int MaxTimer { get; set; } = 7;
            public bool CreateFish { get; set; } = false;

            public void Reset()
            {
                CurrentTimer = MaxTimer;
                CurrentTimer--;
                CreateFish = false;
            }

            public override string ToString()
            {
                return $"{CurrentTimer}";
            }
        }

        private readonly List<Fish> _fish = new List<Fish>();

        private void Day6()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int cycles = 80;
            for (int i = 0; i < cycles; i++)
            {
                List<Fish> newFish = new List<Fish>();
                foreach (var fish in _fish)
                {
                    if (fish.CreateFish)
                    {
                        fish.Reset();
                        newFish.Add(new Fish() { CurrentTimer = 8 });
                    }
                    else
                    {
                        fish.CurrentTimer--;
                        if (fish.CurrentTimer == 0)
                            fish.CreateFish = true;
                    }
                }
                _fish.AddRange(newFish);
                //Console.WriteLine((i + 1) + ": " + string.Join(", ", _fish));
            }

            int ans = _fish.Count;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day06\input.txt";
            var input = File.ReadAllLines(path).First().Split(',').Select(int.Parse).ToList();
            foreach (var n in input)
            {
                _fish.Add(new Fish() { CurrentTimer = n });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
