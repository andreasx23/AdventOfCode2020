using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day14
{
    public class Day14Part1
    {
        class Chemical
        {
            public string Name;
            public List<(int cost, Chemical chemGain, int amountGain)> Produces;
            public Chemical Parent;

            public Chemical()
            {
                Produces = new List<(int cost, Chemical chemGain, int amountGain)>();
            }
        }

        class Combination
        {
            public List<(int cost, string name)> Values;
            public int GainAmount;
            public string GainName;

            public Combination()
            {
                Values = new List<(int cost, string name)>();
            }
        }

        class State
        {
            public int OreCost;
            public Dictionary<Chemical, int> Chemicals;

            public State()
            {
                Chemicals = new Dictionary<Chemical, int>();
            }

            public string Order()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in Chemicals.OrderBy(k => k.Key.Name))
                {
                    sb.Append($"{item.Key.Name}{item.Value}-");
                }
                return sb.ToString();
            }
        }

        private readonly Dictionary<string, Chemical> map = new Dictionary<string, Chemical>();
        private readonly List<Combination> combinations = new List<Combination>();
        private const string FUEL = "FUEL";
        private const string ORE = "ORE";

        private void Day14()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Chemical> chemicals = map.Values.ToList();

            Queue<State> queue = new Queue<State>();
            HashSet<string> visisted = new HashSet<string>();
            foreach (var chemical in chemicals.Where(c => c.Name == ORE))
            {
                foreach (var (cost, chemGain, amountGain) in chemical.Produces)
                {
                    State state = new State()
                    {
                        OreCost = cost
                    };
                    state.Chemicals.Add(chemGain, amountGain);
                    queue.Enqueue(state);
                    visisted.Add(state.Order());
                }
            }

            int ans = 0, runs = 0;
            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Chemicals.Any(c => c.Key.Name == FUEL))
                {
                    ans = current.OreCost;
                    break;
                }

                foreach (var temp in chemicals)
                {
                    if (current.Chemicals.TryGetValue(temp, out int value))
                    {
                        var next = map[temp.Name];
                        foreach (var (cost, chemGain, amountGain) in next.Produces)
                        {
                            if (value >= cost)
                            {
                                State state = new State()
                                {
                                    OreCost = cost,
                                    Chemicals = new Dictionary<Chemical, int>(current.Chemicals)
                                };

                                if (next.Name == ORE) state.OreCost += cost;
                                if (!state.Chemicals.ContainsKey(chemGain)) state.Chemicals.Add(chemGain, 0);

                                state.Chemicals[chemGain] += amountGain;

                                if (visisted.Add(state.Order()))
                                {
                                    queue.Enqueue(state);
                                }
                            }
                        }
                    }
                    else if (temp.Name == ORE)
                    {
                        var next = map[temp.Name];
                        foreach (var (cost, chemGain, amountGain) in next.Produces)
                        {
                            State state = new State()
                            {
                                OreCost = current.OreCost + cost,
                                Chemicals = new Dictionary<Chemical, int>(current.Chemicals)
                            };

                            if (!state.Chemicals.ContainsKey(chemGain)) state.Chemicals.Add(chemGain, 0);
                            state.Chemicals[chemGain] += amountGain;

                            if (visisted.Add(state.Order()))
                            {
                                queue.Enqueue(state);
                            }
                        }
                    }
                }

                foreach (var combination in combinations)
                {
                    bool isOk = true;
                    foreach (var (cost, name) in combination.Values)
                    {
                        var first = current.Chemicals.FirstOrDefault(c => c.Key.Name == name);
                        if (first.Key == null || first.Key != null && first.Value < cost) isOk = false;
                    }

                    if (!isOk) continue;

                    State state = new State()
                    {
                        OreCost = current.OreCost,
                        Chemicals = new Dictionary<Chemical, int>(current.Chemicals)
                    };

                    foreach (var (cost, name) in combination.Values)
                    {
                        var temp = state.Chemicals.FirstOrDefault(c => c.Key.Name == name);
                        state.Chemicals[temp.Key] -= cost;
                    }

                    if (!map.ContainsKey(combination.GainName))
                    {
                        map.Add(combination.GainName, new Chemical() { Name = combination.GainName });
                    }

                    var tmp = map[combination.GainName];
                    if (!state.Chemicals.ContainsKey(tmp)) state.Chemicals.Add(tmp, 0);
                    state.Chemicals[tmp] += combination.GainAmount;
                    
                    if (visisted.Add(state.Order()))
                    {
                        queue.Enqueue(state);
                    }
                }

                runs++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2019\Day14\sample.txt";
            var lines = File.ReadAllLines(path);

            List<string> combinations = new List<string>();
            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var lefts = split.First().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (lefts.Count() > 1)
                {
                    combinations.Add(s);
                    continue;
                }

                var left = split.First().Split(' ');
                var right = split.Last().Split(' ');

                if (!map.ContainsKey(left.Last()))
                {
                    map.Add(left.Last(), new Chemical()
                    {
                        Name = left.Last()
                    });
                }

                if (!map.ContainsKey(right.Last()))
                {
                    map.Add(right.Last(), new Chemical()
                    {
                        Name = right.Last()
                    });
                }

                var parent = map[left.Last()];
                var child = map[right.Last()];

                parent.Produces.Add((int.Parse(left.First()), child, int.Parse(right.First())));
                child.Parent = parent;
            }

            foreach (var s in combinations)
            {
                var split = s.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
                var lefts = split.First().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                var rights = split.Last().Split(' ');

                List<(int cost, string name)> list = new List<(int cost, string name)>();
                foreach (var _s in lefts)
                {
                    var inner = _s.Split(' ');
                    var cost = int.Parse(inner.First());
                    var name = inner.Last();
                    list.Add((cost, name));
                }

                this.combinations.Add(new Combination()
                {
                    Values = list,
                    GainAmount = int.Parse(rights.First()),
                    GainName = rights.Last()
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day14();
        }
    }
}
