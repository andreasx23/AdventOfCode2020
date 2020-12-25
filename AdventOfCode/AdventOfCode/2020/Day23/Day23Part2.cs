using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day23
{
    public class Day23Part2
    {
        private const int input = 962713854;
        private const int sample = 389125467;
        private const int rounds = 10000000;

        private void Day23()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var list = input.ToString().Select(c => int.Parse(c.ToString())).ToList();

            for (int i = 10; i <= 1000000; i++)
            {
                list.Add(i);
            }

            LinkedList<int> cups = new LinkedList<int>(list);

            Dictionary<int, LinkedListNode<int>> map = new Dictionary<int, LinkedListNode<int>>();
            var copyCups = cups.First;
            while (copyCups != null)
            {
                map.Add(copyCups.Value, copyCups);
                copyCups = copyCups.Next;
            }

            var currentCup = cups.First;
            for (int i = 0; i < rounds; i++)
            {
                List<LinkedListNode<int>> picked = new List<LinkedListNode<int>>
                {
                    currentCup.NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst().NextOrFirst()
                };

                foreach (var p in picked)
                {
                    cups.Remove(p);
                }

                var destination = currentCup.Value - 1;
                while (destination < 1 || picked.Any(p => p.Value == destination) || destination == currentCup.Value)
                {
                    destination--;

                    if (destination < 1)
                    {
                        destination = map.Count();
                    }
                }

                currentCup = currentCup.NextOrFirst();
                var target = map[destination];
                foreach (var p in picked)
                {
                    cups.AddAfter(target, p);
                    target = target.NextOrFirst();
                }
            }

            var result = map[1];
            long ans = long.Parse(result.NextOrFirst().Value.ToString()) * long.Parse(result.NextOrFirst().NextOrFirst().Value.ToString());

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        public void TestCase()
        {
            Day23();
        }
    }
}
