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
    public class Day18Part2
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
            char[] operators = new char[] { '-', '*', '/' };

            Stack<char> stack = new Stack<char>();
            while (index != n)
            {
                char c = expression[index];
                if (c == '+')
                {
                    stack.Push(c);
                    index++;
                    while (index != n && char.IsDigit(expression[index]))
                    {
                        stack.Push(expression[index]);
                        index++;
                    }

                    StringBuilder sb = new StringBuilder();
                    while (stack.Count > 0 && !operators.Contains(stack.Peek()))
                    {
                        sb.Append(stack.Pop());
                    }

                    var result = BosMathCalculation(Reverse(sb));
                    foreach (var item in result)
                    {
                        stack.Push(item);
                    }

                    continue;
                }
                else
                {
                    stack.Push(c);
                }

                index++;
            }

            StringBuilder stackValue = new StringBuilder();
            while (stack.Count > 0)
            {
                stackValue.Append(stack.Pop());
            }
            var res = BosMathCalculation(Reverse(stackValue));
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

            if (expression.First() == '(')
            {
                expression = expression.Remove(0, 1);
            }

            if (expression.Last() == ')')
            {
                expression = expression.Remove(expression.Length - 1, 1);
            }

            return expression;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 18\input.txt";
            input = File.ReadAllLines(path).Select(s => s.Replace(" ", "")).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day18();
        }
    }
}
