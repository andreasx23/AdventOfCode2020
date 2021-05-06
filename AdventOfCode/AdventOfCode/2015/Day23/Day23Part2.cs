using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day23
{
    public class Day23Part2
    {
        private List<string> commands = new List<string>();

        private void Day23()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int a = 1, b = 0, index = 0;
            while (index < commands.Count)
            {
                var current = commands[index];
                if (current.Contains("hlf"))
                {
                    var split = current.Split(' ').ToList();

                    if (split.Last() == "a")
                    {
                        a /= 2;
                    }
                    else
                    {
                        b /= 2;
                    }
                }
                else if (current.Contains("tpl"))
                {
                    var split = current.Split(' ').ToList();

                    if (split.Last() == "a")
                    {
                        a *= 3;
                    }
                    else
                    {
                        b *= 3;
                    }
                }
                else if (current.Contains("inc"))
                {
                    var split = current.Split(' ').ToList();

                    if (split.Last() == "a")
                    {
                        a += 1;
                    }
                    else
                    {
                        b += 1;
                    }
                }
                else if (current.Contains("jmp"))
                {
                    var split = current.Split(' ').ToList();

                    if (split.Last().Contains("-"))
                    {
                        index -= Math.Abs(int.Parse(split.Last()));
                    }
                    else
                    {
                        index += int.Parse(split.Last());
                    }
                    continue;
                }
                else if (current.Contains("jie"))
                {
                    var split = current.Replace(",", "").Split(' ').ToList();

                    if (split[1] == "a" && a % 2 == 0 || split[1] == "b" && b % 2 == 0)
                    {
                        if (split.Last().Contains("-"))
                        {
                            index -= Math.Abs(int.Parse(split.Last()));
                        }
                        else
                        {
                            index += int.Parse(split.Last());
                        }
                        continue;
                    }
                }
                else if (current.Contains("jio"))
                {
                    var split = current.Replace(",", "").Split(' ').ToList();

                    if (split[1] == "a" && a == 1 || split[1] == "b" && b == 1)
                    {
                        if (split.Last().Contains("-"))
                        {
                            index -= Math.Abs(int.Parse(split.Last()));
                        }
                        else
                        {
                            index += int.Parse(split.Last());
                        }
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: " + index + " " + current);
                    break;
                }

                index++;
            }

            int ans = b;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day23\input.txt";
            commands = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day23();
        }
    }
}
