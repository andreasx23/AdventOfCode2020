using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day10
{
    public class Day10Part2
    {
        private readonly Dictionary<int, Bot> bots = new Dictionary<int, Bot>();

        private void Day10()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<Bot> order = bots.Values.OrderByDescending(b => b.ValueChips.Count).ToList();
            List<int> output0 = new List<int>(), output1 = new List<int>(), output2 = new List<int>();
            while (order.Any(b => b.ValueChips.Count == 2))
            {
                Bot current = order.First();

                int min = current.ValueChips.Min();
                int max = current.ValueChips.Max();

                if (current.HasLowBot())
                {
                    current.LowBot.ValueChips.Add(min);
                }

                if (current.HasHighBot())
                {
                    current.HighBot.ValueChips.Add(max);
                }

                if (current.LowOutput == 0)
                {
                    output0.Add(min);
                }
                else if (current.LowOutput == 1)
                {
                    output1.Add(min);
                }
                else if (current.LowOutput == 2)
                {
                    output2.Add(min);
                }

                if (current.HighOutput == 0)
                {
                    output0.Add(max);
                }
                else if (current.HighOutput == 1)
                {
                    output1.Add(max);
                }
                else if (current.HighOutput == 2)
                {
                    output2.Add(max);
                }

                current.ValueChips.Remove(min);
                current.ValueChips.Remove(max);

                order = order.OrderByDescending(b => b.ValueChips.Count).ToList();
            }

            long ans = output0.First() * output1.First() * output2.First();

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print()
        {
            foreach (var bot in bots.Values)
            {
                Console.WriteLine(bot.Id + " " + bot.LowOutput + " " + bot.HighOutput + " " + bot.HasLowBot() + " " + bot.HasHighBot());
                Console.WriteLine(string.Join(" ", bot.ValueChips));
                Console.WriteLine();
            }
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day10\input.txt";
            var lines = File.ReadAllLines(path);

            List<(Bot bot, int lowBotId, int highBotId)> list = new List<(Bot bot, int lowBotId, int highBotId)>();
            foreach (var s in lines)
            {
                if (s.StartsWith("bot"))
                {
                    Bot current = new Bot();
                    string[] split = s.Split(new string[] { "gives" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                    int botId = int.Parse(split[0].Split(' ')[1]);
                    current.Id = botId;
                    string[] lowAndHigh = split[1].Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

                    bool hasBot = false;
                    int lowerBotId = -1, higherBotId = -1;
                    if (lowAndHigh[0].Contains("bot"))
                    {
                        int lowBotId = int.Parse(lowAndHigh[0].Split(new string[] { "bot" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[1]);
                        lowerBotId = lowBotId;
                        hasBot = true;
                    }
                    else
                    {
                        int lowOutput = int.Parse(lowAndHigh[0].Split(new string[] { "output" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[1]);
                        current.LowOutput = lowOutput;
                    }

                    if (lowAndHigh[1].Contains("bot"))
                    {
                        int highBotId = int.Parse(lowAndHigh[1].Split(new string[] { "bot" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[1]);
                        higherBotId = highBotId;
                        hasBot = true;
                    }
                    else
                    {
                        int highOutput = int.Parse(lowAndHigh[1].Split(new string[] { "output" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[1]);
                        current.HighOutput = highOutput;
                    }

                    if (hasBot)
                    {
                        list.Add((current, lowerBotId, higherBotId));
                    }

                    bots.Add(current.Id, current);
                }
            }

            foreach (var s in lines)
            {
                if (s.StartsWith("value"))
                {
                    var temp = s.Substring(5);
                    var split = temp.Split(new string[] { " goes to bot " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    int value = int.Parse(split[0]);
                    int botId = int.Parse(split[1]);
                    Bot current = bots[botId];
                    current.ValueChips.Add(value);
                }
            }

            var order = list.OrderBy(kv => kv.bot.Id);
            foreach (var (bot, lowBotId, highBotId) in order)
            {
                Bot current = bots[bot.Id];
                if (lowBotId > -1)
                {
                    current.LowBot = bots[lowBotId];
                }

                if (highBotId > -1)
                {
                    current.HighBot = bots[highBotId];
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day10();
        }
    }
}
