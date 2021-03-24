using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day09
{
    public class Day9Part2
    {
        private string input = string.Empty;

        private void Day9()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long ans = 0;
            StringBuilder marker = new StringBuilder();
            bool isMarker = false;
            for (int i = 0; i < input.Length - 1; i++)
            {
                char c = input[i];

                Console.WriteLine("Current index: " + i);

                if (c == '(' && char.IsDigit(input[i + 1]))
                {
                    isMarker = true;
                }
                else if (isMarker && c == ')')
                {
                    List<int> split = marker.ToString().Split('x').Select(int.Parse).ToList();
                    marker.Clear();
                    isMarker = false;

                    int amount = split[0];
                    int turns = split[1];

                    int iAmountToJump = amount;

                    StringBuilder compressed = new StringBuilder();
                    for (int j = 0; j < turns; j++)
                    {
                        StringBuilder sub = new StringBuilder();
                        for (int k = 0; k < amount; k++)
                        {
                            sub.Append(input[i + 1 + k]);
                        }
                        compressed.Append(sub);
                    }

                    int rounds = 1;
                    if (compressed.ToString().Contains("("))
                    {
                        while (compressed.ToString().Contains("("))
                        {
                            Console.WriteLine("Round count: " + rounds++);
                            List<(int _turns, StringBuilder section)> innerCompressions = new List<(int _turns, StringBuilder section)>();

                            StringBuilder currentSection = new StringBuilder();

                            for (int j = 0; j < compressed.Length; j++)
                            {
                                c = compressed[j];

                                if (c == '(')
                                {
                                    isMarker = true;
                                }
                                else if (isMarker && c == ')')
                                {
                                    split = currentSection.ToString().Split('x').Select(int.Parse).ToList();
                                    currentSection.Clear();
                                    isMarker = false;

                                    amount = split[0];
                                    turns = split[1];

                                    StringBuilder sub = new StringBuilder();
                                    for (int k = 0; k < amount; k++)
                                    {
                                        sub.Append(compressed[j + 1 + k]);
                                    }
                                    innerCompressions.Add((turns, sub));

                                    j += amount;
                                }
                                else if (isMarker)
                                {
                                    currentSection.Append(c);
                                }
                                else
                                {
                                    ans++;
                                }
                            }

                            Console.WriteLine("Curret index: " + i + " inner compressions: " + innerCompressions.Count);
                            
                            StringBuilder toCheck = new StringBuilder();
                            for (int j = 0; j < innerCompressions.Count; j++)
                            {
                                (int _turns, StringBuilder section) = innerCompressions[j];

                                if (j % 250 == 0)
                                {
                                    Console.WriteLine($"Curret index: {i} - Done with: {j} out of inner compressions: {innerCompressions.Count}");
                                }

                                for (int k = 0; k < _turns; k++)
                                {
                                    toCheck.Append(section);
                                }
                            }

                            compressed = new StringBuilder(toCheck.ToString());
                        }
                    }

                    ans += compressed.Length;

                    i += iAmountToJump;
                }
                else if (isMarker)
                {
                    marker.Append(c);
                }
                else
                {
                    ans++;
                }
            }

            Console.WriteLine("Answer:      11_797_310_782");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
            Console.ReadLine();
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2016\Day09\input.txt";
            input = File.ReadAllLines(path)[0];
        }

        public void TestCase()
        {
            ReadData();
            Day9();
        }
    }
}
