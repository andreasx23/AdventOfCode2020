using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day22
{
    public class Day22Part2
    {
        private readonly Queue<int> playerOne = new Queue<int>();
        private readonly Queue<int> playerTwo = new Queue<int>();

        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var result = Recursion(playerOne, playerTwo);
            var list = (result.Item1.Count > 0) ? result.Item1.ToList() : result.Item2.ToList();
            list.Reverse();
            int ans = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ans += list[i] * (i + 1);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private (Queue<int>, Queue<int>) Recursion(Queue<int> player1, Queue<int> player2)
        {
            HashSet<string> prevRoundPlayer1 = new HashSet<string>(), prevRoundPlayer2 = new HashSet<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                string currentStatePlayerOne = string.Join(" ", player1.Select(num => num.ToString()));
                string currentStatePlayerTwo = string.Join(" ", player2.Select(num => num.ToString()));
                if (prevRoundPlayer1.Contains(currentStatePlayerOne) || prevRoundPlayer2.Contains(currentStatePlayerTwo))
                {
                    return (player1, new Queue<int>());
                }
                else
                {
                    prevRoundPlayer1.Add(currentStatePlayerOne);
                    prevRoundPlayer2.Add(currentStatePlayerTwo);
                }
                
                int p1Card = player1.Dequeue(), p2Card = player2.Dequeue();
                bool isPlayer1 = p1Card > p2Card;

                if (player1.Count >= p1Card && player2.Count >= p2Card)
                {
                    Queue<int> subQP1 = new Queue<int>(player1.Take(p1Card)), subQP2 = new Queue<int>(player2.Take(p2Card));
                    var result = Recursion(subQP1, subQP2);
                    isPlayer1 = result.Item1.Count > 0;
                }

                if (isPlayer1)
                {
                    player1.Enqueue(p1Card);
                    player1.Enqueue(p2Card);
                }
                else
                {
                    player2.Enqueue(p2Card);
                    player2.Enqueue(p1Card);
                }
            }

            return (player1, player2);
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 22\input.txt";
            var lines = File.ReadAllLines(path);

            int index = 1;
            while (!string.IsNullOrEmpty(lines[index]))
            {
                playerOne.Enqueue(int.Parse(lines[index]));
                index++;
            }

            for (int i = index + 2; i < lines.Length; i++)
            {
                playerTwo.Enqueue(int.Parse(lines[i]));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}