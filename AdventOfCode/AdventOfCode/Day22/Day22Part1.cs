using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day22
{
    public class Day22Part1
    {
        private readonly Queue<int> playerOne = new Queue<int>();
        private readonly Queue<int> playerTwo = new Queue<int>();

        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (playerOne.Count > 0 && playerTwo.Count > 0)
            {
                int playerOneCard = playerOne.Dequeue(), playerTwoCard = playerTwo.Dequeue();

                if (playerOneCard > playerTwoCard)
                {
                    playerOne.Enqueue(playerOneCard);
                    playerOne.Enqueue(playerTwoCard);
                }
                else
                {
                    playerTwo.Enqueue(playerTwoCard);
                    playerTwo.Enqueue(playerOneCard);                    
                }
            }

            var toLoop = (playerOne.Count > 0) ? playerOne.ToList() : playerTwo.ToList();
            int ans = 0, index = 1;
            for (int i = toLoop.Count - 1; i >= 0; i--)
            {
                ans += toLoop[i] * index;
                index++;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
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

            index += 2;
            for (int i = index; i < lines.Length; i++)
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
