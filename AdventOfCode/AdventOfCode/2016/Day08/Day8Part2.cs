using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day08
{
    public class Day8Part2
    {
        private readonly List<Entity> input = new List<Entity>();
        private readonly bool isSample = false;

        private void Day8()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            char[][] grid = new char[isSample ? 3 : 6][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new char[isSample ? 7 : 50];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = '.';
                }
            }

            foreach (var entity in input)
            {
                if (entity.Type == Type.rect)
                {
                    grid = RectAxB(grid, entity.A, entity.B);
                }
                else if (entity.Type == Type.row)
                {
                    grid = RotateRow(grid, entity.Row, entity.Amount);
                }
                else
                {
                    grid = RotateColumn(grid, entity.Column, entity.Amount);
                }
            }

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '.')
                    {
                        grid[i][j] = ' ';
                    }
                }
            }

            Print(grid);

            watch.Stop();
            Console.WriteLine($"Answer: {"AFBUPZBJPS"} took {watch.ElapsedMilliseconds} ms");
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
        }

        private char[][] RectAxB(char[][] grid, int a, int b)
        {
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    grid[i][j] = '#';
                }
            }
            return grid;
        }

        private char[][] RotateColumn(char[][] grid, int column, int amount)
        {
            char[] current = new char[isSample ? 3 : 6];
            int n = current.Length;
            for (int i = 0; i < n; i++)
            {
                current[i] = grid[i][column];
            }

            for (int i = 0; i < amount; i++)
            {
                char[] temp = (char[])current.Clone();
                for (int j = 0; j < n; j++)
                {
                    temp[(j + 1) % n] = current[j];
                }
                current = temp;
            }

            for (int i = 0; i < n; i++)
            {
                grid[i][column] = current[i];
            }

            return grid;
        }

        private char[][] RotateRow(char[][] grid, int row, int amount)
        {
            char[] current = grid[row];
            int n = current.Length;

            for (int i = 0; i < amount; i++)
            {
                char[] temp = (char[])current.Clone();
                for (int j = 0; j < n; j++)
                {
                    temp[(j + 1) % n] = current[j];
                }
                current = temp;
            }

            grid[row] = current;

            return grid;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2016\Day08\input.txt";
            var lines = File.ReadAllLines(path).ToList();

            foreach (var s in lines)
            {
                string temp = string.Empty;
                Entity current = new Entity();
                if (s.Contains("rect"))
                {
                    temp = s.Replace("rect ", "");
                    List<int> split = temp.Split('x').Select(int.Parse).ToList();
                    current.Type = Type.rect;
                    current.A = split[0];
                    current.B = split[1];
                }
                else if (s.Contains("row"))
                {
                    temp = s.Replace("rotate row y=", "");
                    List<int> split = temp.Split(new string[] { " by " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    current.Type = Type.row;
                    current.Row = split[0];
                    current.Amount = split[1];
                }
                else if (s.Contains("column"))
                {
                    temp = s.Replace("rotate column x=", "");
                    List<int> split = temp.Split(new string[] { " by " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    current.Type = Type.column;
                    current.Column = split[0];
                    current.Amount = split[1];
                }
                input.Add(current);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day8();
        }
    }
}
