using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day14
{
    public class Day14Part2
    {
        class Chemical
        {
            public string Name;
            public int Amount;

            public override string ToString()
            {
                return $"Name: {Name}, Amount: {Amount}";
            }
        }

        class Product
        {
            public Chemical Chemical;
            public List<Chemical> Childs = new List<Chemical>();
        }

        private readonly Dictionary<string, Product> map = new Dictionary<string, Product>();
        private readonly Dictionary<string, int> bank = new Dictionary<string, int>();
        private const string FUEL = "FUEL";
        private const string ORE = "ORE";
        private int temp = 0;
        private long balance = 1000000000000;

        private void Day14()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            var root = map[FUEL];
            while (balance > 0)
            {
                temp = 0;
                foreach (var child in root.Childs)
                {
                    Produce(child);
                }
                balance -= temp;

                if (ans % 1000 == 0)
                {
                    Console.WriteLine(ans + " " + balance);
                }
                ans++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Produce(Chemical root)
        {
            if (root.Name == ORE)
            {
                temp += root.Amount;
                return;
            }

            if (bank.ContainsKey(root.Name))
            {
                if (bank[root.Name] >= root.Amount)
                {
                    bank[root.Name] -= root.Amount;
                    return;
                }
                else
                {
                    root.Amount -= bank[root.Name];
                    bank[root.Name] = 0;
                }
            }

            Product current = map[root.Name];

            int repeat = (int)Math.Ceiling((double)root.Amount / current.Chemical.Amount);

            foreach (var child in current.Childs)
            {
                Produce(new Chemical() { Amount = child.Amount * repeat, Name = child.Name });
            }

            int calculation = current.Chemical.Amount * repeat;
            if (calculation > root.Amount)
            {
                if (bank.ContainsKey(current.Chemical.Name))
                {
                    bank[current.Chemical.Name] += calculation - root.Amount;
                }
                else
                {
                    bank.Add(current.Chemical.Name, calculation - root.Amount);
                }
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2019\Day14\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var lefts = split.First().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var right = split.Last().Split(' ');

                Product product = new Product()
                {
                    Chemical = new Chemical() { Name = right.Last(), Amount = int.Parse(right.First()) }
                };

                List<Chemical> childs = new List<Chemical>();
                foreach (var _ in lefts)
                {
                    var split2 = _.Split(' ').ToArray();
                    childs.Add(new Chemical()
                    {
                        Name = split2.Last(),
                        Amount = int.Parse(split2.First())
                    });
                }
                product.Childs = childs;

                map.Add(product.Chemical.Name, product);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day14();
        }
    }
}
