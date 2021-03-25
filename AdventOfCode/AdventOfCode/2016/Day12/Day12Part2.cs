using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day12
{
    public class Day12Part2
    {
        private List<string> input = new List<string>();

        private void Day12()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int a = 0, b = 0, c = 1, d = 0, i = 0;
            bool isFinished = false;
            while (!isFinished)
            {
                var s = input[i];
                var split = s.Split(' ');

                bool isJnz = false;
                if (split[0].Contains("cpy"))
                {
                    if (char.IsDigit(split[1].First()))
                    {
                        switch (split[2][0])
                        {
                            case 'a':
                                a = int.Parse(split[1]);
                                break;
                            case 'b':
                                b = int.Parse(split[1]);
                                break;
                            case 'c':
                                c = int.Parse(split[1]);
                                break;
                            case 'd':
                                d = int.Parse(split[1]);
                                break;
                            default:
                                Console.WriteLine("CPY failed no digit");
                                throw new Exception();
                        }
                    }
                    else
                    {
                        switch (split[1][0])
                        {
                            case 'a':
                                {
                                    switch (split[2][0])
                                    {
                                        case 'b':
                                            b = a;
                                            break;
                                        case 'c':
                                            c = a;
                                            break;
                                        case 'd':
                                            d = a;
                                            break;
                                        default:
                                            Console.WriteLine("CPY failed set value to A");
                                            throw new Exception();
                                    }
                                }
                                break;
                            case 'b':
                                {
                                    switch (split[2][0])
                                    {
                                        case 'a':
                                            a = b;
                                            break;
                                        case 'c':
                                            c = b;
                                            break;
                                        case 'd':
                                            d = b;
                                            break;
                                        default:
                                            Console.WriteLine("CPY failed set value to B");
                                            throw new Exception();
                                    }
                                }
                                break;
                            case 'c':
                                {
                                    switch (split[2][0])
                                    {
                                        case 'a':
                                            a = c;
                                            break;
                                        case 'b':
                                            b = c;
                                            break;
                                        case 'd':
                                            d = c;
                                            break;
                                        default:
                                            Console.WriteLine("CPY failed set value to C");
                                            throw new Exception();
                                    }
                                }
                                break;
                            case 'd':
                                {
                                    switch (split[2][0])
                                    {
                                        case 'a':
                                            a = d;
                                            break;
                                        case 'b':
                                            b = d;
                                            break;
                                        case 'c':
                                            c = d;
                                            break;
                                        default:
                                            Console.WriteLine("CPY failed set value to D");
                                            throw new Exception();
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("CPY failed no such value");
                                throw new Exception();
                        }
                    }
                }
                else if (split[0].Contains("inc"))
                {
                    switch (split[1][0])
                    {
                        case 'a':
                            a++;
                            break;
                        case 'b':
                            b++;
                            break;
                        case 'c':
                            c++;
                            break;
                        case 'd':
                            d++;
                            break;
                        default:
                            Console.WriteLine("INC failed");
                            throw new Exception();
                    }
                }
                else if (split[0].Contains("dec"))
                {
                    switch (split[1][0])
                    {
                        case 'a':
                            a--;
                            break;
                        case 'b':
                            b--;
                            break;
                        case 'c':
                            c--;
                            break;
                        case 'd':
                            d--;
                            break;
                        default:
                            Console.WriteLine("DEC failed");
                            throw new Exception();
                    }
                }
                else if (split[0].Contains("jnz"))
                {
                    switch (split[1][0])
                    {
                        case 'a':
                            {
                                if (a != 0)
                                {
                                    isJnz = true;
                                    i += int.Parse(split[2]);
                                }
                            }
                            break;
                        case 'b':
                            {
                                if (b != 0)
                                {
                                    isJnz = true;
                                    i += int.Parse(split[2]);
                                }
                            }
                            break;
                        case 'c':
                            {
                                if (c != 0)
                                {
                                    isJnz = true;
                                    i += int.Parse(split[2]);
                                }
                            }
                            break;
                        case 'd':
                            {
                                if (d != 0)
                                {
                                    isJnz = true;
                                    i += int.Parse(split[2]);
                                }
                            }
                            break;
                        default:
                            {
                                if (int.Parse(split[1]) != 0)
                                {
                                    isJnz = true;
                                    i += int.Parse(split[2]);
                                }
                            }
                            break;
                    }
                }

                if (!isJnz)
                {
                    i++;
                }

                if (i >= input.Count)
                {
                    isFinished = true;
                }
            }

            int ans = a;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day12\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day12();
        }
    }
}
