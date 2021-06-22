using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day19
{
    public class Day19Part1
    {
        class Elf
        {
            public int Id;
            public int Presents;
            public Elf Next;
        }

        private readonly int input = 3001330;

        private void Day19()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Queue<Elf> queue = new Queue<Elf>();
            List<Elf> elfs = new List<Elf>();
            for (int i = 0; i < input; i++)
            {
                var elf = new Elf() { Id = i + 1, Presents = 1 };
                queue.Enqueue(elf);
                elfs.Add(elf);
            }

            for (int i = 1; i < input; i++)
            {
                Elf prev = elfs[i - 1], current = elfs[i];
                prev.Next = current;
            }
            elfs.Last().Next = elfs.First();

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Presents == 0) continue;

                var next = current.Next;
                while (next.Presents == 0) next = next.Next;

                if (current.Id != next.Id)
                {
                    current.Presents += next.Presents;
                    current.Next = next;
                    next.Presents = 0;
                    queue.Enqueue(current);
                }
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
