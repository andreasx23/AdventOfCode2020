using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day12
{
    public class Day12Part2
    {
        private string input = string.Empty;

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StringBuilder sb = new StringBuilder(), paranSb = new StringBuilder();
            bool isParan = false;

            int count = 0;
            foreach (var c in input)
            {
                if (!isParan)
                {
                    if (c == '{')
                    {
                        paranSb.Append(c);
                        isParan = true;
                        count++;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    paranSb.Append(c);

                    if (c == '{') 
                    {
                        count++;
                    }
                    else if (c == '}')
                    {
                        count--;

                        if (count == 0)
                        {
                            if (!paranSb.ToString().Contains("red"))
                            {
                                sb.Append(paranSb);
                            }
                            else
                            {
                                StringBuilder outerParan = new StringBuilder(), innerParan = new StringBuilder();
                                bool isInnerParan = false;
                                foreach (var _c in paranSb.ToString())
                                {
                                    if (isInnerParan)
                                    {
                                        innerParan.Append(_c);
                                    }
                                    else
                                    {
                                        outerParan.Append(_c);
                                    }

                                    if (_c == '{')
                                    {
                                        if (count > 0)
                                        {
                                            isInnerParan = true;
                                        }
                                        count++;
                                    }
                                    else if (count > 0 && _c == '}')
                                    {
                                        if (!innerParan.ToString().Contains("red"))
                                        {
                                            outerParan.Append(innerParan);
                                        }
                                        innerParan.Clear();
                                        isInnerParan = false;
                                        count--;
                                    }
                                }

                                if (!outerParan.ToString().Contains("red"))
                                {
                                    Console.WriteLine(outerParan);
                                    sb.Append(outerParan);
                                }
                            }

                            paranSb.Clear();
                            isParan = false;
                        }
                    }
                }
            }

            string temp = "";
            int ans = 0;
            foreach (var c in sb.ToString())
            {
                if (c == '-' || char.IsDigit(c))
                {
                    temp += c.ToString();
                }
                else if (!string.IsNullOrEmpty(temp))
                {
                    ans += int.Parse(temp);
                    temp = "";
                }
            }

            Console.WriteLine("15452 > && < 94184");
            Console.WriteLine("Is not 19147");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day12\input.txt";
            input = File.ReadAllLines(path)[0].ToLower();
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
