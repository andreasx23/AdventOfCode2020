using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day19
{
    public class Day19Part2
    {
        class Elf
        {
            public int Id;
            public int Presents;
            public Elf Prev;
            public Elf Next;
        }

        private readonly int input = 3001330;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Elf> elfs = new List<Elf>();
            for (int i = 0; i < input; i++)
            {
                var elf = new Elf() { Id = i + 1, Presents = 1 };
                elfs.Add(elf);
            }

            for (int i = 1; i < input; i++)
            {
                Elf prev = elfs[i - 1], current = elfs[i];
                current.Prev = prev;
                prev.Next = current;                
            }
            elfs.First().Prev = elfs.Last();
            elfs.Last().Next = elfs.First();

            var start = elfs.First();
            var mid = elfs[input / 2];
            for (int i = 0; i < input - 1; i++)
            {
                start.Presents += mid.Presents;
                mid.Presents = 0;
                mid.Prev.Next = mid.Next;
                mid.Next.Prev = mid.Prev;
                mid = ((input - i) % 2 == 0) ? mid.Next : mid.Next.Next;
                start = start.Next;
            }

            int ans = elfs.First(e => e.Presents > 0).Id;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            //string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day18\input.txt";
            //input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day19();
        }
    }
}
