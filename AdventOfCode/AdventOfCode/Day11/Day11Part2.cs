using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
{
    public class Day11Part2
    {
        private List<char[]> input = new List<char[]>();

        /*
         * L = Empty
         * # = occupied
         * . = floor
         * 
         * https://adventofcode.com/2020/day/11#part2
         */
        private void Day11()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<char[]> currentState = new List<char[]>();
            int i = 0, j = 0, column = input.Count - 1, row = input[0].Length - 1, turnCounter = 0;

            for (int index = 0; index <= column; index++)
            {
                char[] arr = new char[row + 1];
                currentState.Add(arr);
            }

            while (true)
            {
                int seatCounter = 0;
                bool isEmpty = true;
                if (j > 0) //Left
                {
                    int k = j - 1;
                    while (k != - 1)
                    {
                        char c = input[i][k];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k--;
                    }
                }

                if (j != row) //Right
                {
                    int k = j + 1;
                    while (k != row + 1)
                    {
                        char c = input[i][k];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k++;
                    }
                }

                if (i > 0) //Up
                {
                    int k = i - 1;
                    while (k != -1)
                    {
                        char c = input[k][j];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k--;
                    }
                }

                if (i != column) //Down
                {
                    int k = i + 1;
                    while (k != column + 1)
                    {
                        char c = input[k][j];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k++;
                    }
                }

                if (i > 0 && j > 0) //Upper left
                {
                    int k = i - 1, x = j - 1;
                    while (k != -1 && x != -1)
                    {
                        char c = input[k][x];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k--;
                        x--;
                    }
                }

                if (i > 0 && j != row) //Upper right
                {
                    int k = i - 1, x = j + 1;
                    while (k != -1 && x != row + 1)
                    {
                        char c = input[k][x];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k--;
                        x++;
                    }
                }

                if (i != column && j > 0) //Lower left
                {
                    int k = i + 1, x = j - 1;
                    while (k != column + 1 && x != -1)
                    {
                        char c = input[k][x];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k++;
                        x--;
                    }
                }

                if (i != column && j != row) //Lower right
                {
                    int k = i + 1, x = j + 1;
                    while (k != column + 1 && x != row + 1)
                    {
                        char c = input[k][x];
                        if (c == 'L') //Empty
                        {
                            break;
                        }
                        else if (c == '#') //occupied
                        {
                            isEmpty = false;
                            seatCounter++;
                            break;
                        }
                        k++;
                        x++;
                    }
                }

                if (input[i][j] == '.')
                {
                    currentState[i][j] = '.';
                }
                else if (isEmpty)
                {
                    currentState[i][j] = '#';
                }
                else if (seatCounter >= 5)
                {
                    currentState[i][j] = 'L';
                }

                j++;
                if (i == column && j == row + 1) //Reset
                {
                    if (CheckEquilibrium(input, currentState))
                    {
                        break;
                    }

                    input = new List<char[]>();
                    for (int index = 0; index <= column; index++)
                    {
                        int n = row + 1;
                        char[] arr = new char[n];                        
                        for (int indexJ = 0; indexJ < n; indexJ++)
                        {
                            arr[indexJ] = currentState[index][indexJ];
                        }
                        input.Add(arr);
                    }

                    turnCounter++;
                    i = 0;
                    j = 0;
                }
                else if (j == row + 1)
                {
                    i++;
                    j = 0;
                }
            }

            int ans = 0;
            currentState.ForEach(s =>
            {
                ans += s.Count(c => c == '#');
            });

            watch.Stop();
            Console.WriteLine("Answer: " + ans + " took " + watch.ElapsedMilliseconds + " ms");
        }

        private void Print(List<char[]> input, int i, int j)
        {
            var copy = new List<char[]>();
            int column = input.Count, row = input[0].Length;

            for (int index = 0; index < column; index++)
            {
                int n = row;
                char[] arr = new char[n];
                for (int indexJ = 0; indexJ < n; indexJ++)
                {
                    arr[indexJ] = input[index][indexJ];
                }
                copy.Add(arr);
            }

            copy[i][j] = 'X';
            foreach (var array in copy)
            {
                Console.WriteLine(string.Join("", array));
            }
            Console.WriteLine();
        }

        private bool CheckEquilibrium(List<char[]> input, List<char[]> currentState)
        {
            int column = input.Count, row = currentState.Count;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (input[i][j] != currentState[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\Advent of code\Day 11\input.txt";

            foreach (var s in File.ReadAllLines(path))
            {
                int n = s.Length;
                char[] arr = new char[n];
                for (int index = 0; index < n; index++)
                {
                    arr[index] = s[index];
                }
                input.Add(arr);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day11();
        }
    }
}
