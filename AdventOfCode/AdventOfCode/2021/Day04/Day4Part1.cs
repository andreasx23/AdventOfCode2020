using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day04
{
    public class Day4Part1
    {
        private List<int> _numbers = new List<int>();
        private readonly List<(List<List<int>> board, bool[,] isVisited)> _bingoBoards = new List<(List<List<int>> board, bool[,] isVisited)>();

        private void Day4()
        {
            Stopwatch watch = Stopwatch.StartNew();

            int ans = 0;
            foreach (var current in _numbers)
            {
                //Console.WriteLine("Number: " + current);
                bool isWinner = false;
                foreach (var (board, isVisited) in _bingoBoards)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        List<int> row = board[i]; 
                        int index = row.IndexOf(current);
                        if (index != -1)
                        {
                            isVisited[i, index] = true;
                            break;
                        }
                    }

                    if (IsWinner(isVisited))
                    {
                        isWinner = true;
                        int sum = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (!isVisited[i, j])
                                    sum += board[i][j];
                            }
                        }

                        ans = sum * current;
                        break;
                    }
                }

                if (isWinner) 
                    break;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.Elapsed} ms");
        }

        private bool IsWinner(bool[,] isVisited)
        {
            for (int i = 0; i < 5; i++)
            {
                int row = 0, column = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (isVisited[i, j])
                        row++;
                    if (isVisited[j, i])
                        column++;
                }
                if (row == 5 || column == 5)
                    return true;
            }
            return false;
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2021\Day04\input.txt";
            var input = File.ReadAllLines(path).Where(s => !string.IsNullOrEmpty(s));
            _numbers = input.First().Split(',').Select(int.Parse).ToList();
            var boards = input.Skip(1).Select(s => s.Trim().Replace("  ", " ").Split(' ').Select(int.Parse).ToList()).ToList();
            for (int i = 0; i < boards.Count; i += 5)
            {
                List<int> row1 = boards[i];
                List<int> row2 = boards[i + 1];
                List<int> row3 = boards[i + 2];
                List<int> row4 = boards[i + 3];
                List<int> row5 = boards[i + 4];
                List<List<int>> temp = new List<List<int>>()
                {
                    row1,
                    row2,
                    row3,
                    row4,
                    row5,
                };
                bool[,] isVisited = new bool[5, row1.Count];
                _bingoBoards.Add((temp, isVisited));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day4();
        }
    }
}
