using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day18
{
    public class Day18Part1
    {
        private List<string> input = new List<string>();

        private void Day18()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            BigInteger ans = 0;
            foreach (var s in input)
            {
                Stack<char> stack = new Stack<char>();
                if (s.Contains('('))
                {
                    int index = 0, n = s.Length, parenCounter = 0;
                    while (index != n)
                    {
                        char c = s[index];
                        if (c == '(')
                        {
                            parenCounter++;
                            stack.Push(c);
                        }
                        else if (parenCounter > 0 && c == ')')
                        {
                            stack.Push(c);

                            StringBuilder sb = new StringBuilder();
                            while (stack.Peek() != '(')
                            {
                                sb.Append(stack.Pop());
                            }
                            parenCounter--;
                            sb.Append(stack.Pop());

                            string expression = Reverse(sb);
                            foreach (var digit in WeirdCalculation(expression))
                            {
                                stack.Push(digit);
                            }
                        }
                        else
                        {
                            stack.Push(c);
                        }
                        index++;
                    }
                }
                else
                {
                    foreach (var c in s)
                    {
                        stack.Push(c);
                    }
                }

                StringBuilder resultOfS = new StringBuilder();
                while (stack.Count > 0)
                {
                    resultOfS.Append(stack.Pop());
                }
                string reverse = Reverse(resultOfS);                
                string calc = WeirdCalculation(reverse);                
                ans += BigInteger.Parse(calc);

                //Console.WriteLine(s + " == " + calc);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string WeirdCalculation(string expression)
        {
            int index = 0, n = expression.Length;
            bool hasLeft = false;
            StringBuilder left = new StringBuilder(), right = new StringBuilder();

            char validator = '?';
            char[] validators = new char[] { '+', '-', '*', '/' };
            while (index != n)
            {
                char c = expression[index];
                if (validators.Contains(c))
                {
                    if (hasLeft)
                    {
                        string result = BosMathCalculation($"{left} {validator} {right}");
                        left = new StringBuilder(result);
                        right.Clear();
                    }
                    
                    validator = c;
                    hasLeft = true;
                }
                else if (!hasLeft)
                {
                    left.Append(c);
                }
                else
                {
                    right.Append(c);
                }

                index++;
            }

            var res = BosMathCalculation($"{left} {validator} {right}");
            return res;
        }

        //Bosmath calculation
        private string BosMathCalculation(string expression)
        {
            char[] separators = new char[4] { '+', '-', '*', '/' };
            string[] numbers = expression.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < numbers.Length; i++)
            {
                Regex r1 = new Regex(numbers[i]);
                expression = r1.Replace(expression, "X", 1);
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                decimal d = Convert.ToDecimal(numbers[i]);
                numbers[i] = d.ToString("0.####");//# 0-28

                if (!numbers[i].Contains("."))
                {
                    numbers[i] += ".0";
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                Regex r2 = new Regex("X");
                expression = r2.Replace(expression, numbers[i], 1);
            }

            decimal dec = Convert.ToDecimal(new DataTable().Compute(expression, ""));
            var result = dec.ToString("0.####"); //# 0-28

            return result;
        }

        //Removes parans if any
        private string Reverse(StringBuilder sb)
        {
            string expression = new string(sb.ToString().Reverse().ToArray());

            if (expression[0] == '(')
            {
                expression = expression.Remove(0, 1);
            }

            int toRemove = expression.Length - 1;
            if (expression[toRemove] == ')')
            {
                expression = expression.Remove(toRemove, 1);
            }

            return expression;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\Day 18\input.txt";
            input = File.ReadAllLines(path).Select(s => s.Replace(" ", "")).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
