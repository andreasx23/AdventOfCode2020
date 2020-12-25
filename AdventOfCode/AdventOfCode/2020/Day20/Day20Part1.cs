using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day20
{
    public class Day20Part1
    {
        private Dictionary<string, List<List<char[]>>> map = new Dictionary<string, List<List<char[]>>>();
        private List<string> names = new List<string>();

        //Unsolved
        private void Day20()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = 0;
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 20\sample.txt";
            var lines = File.ReadAllLines(path);

            List<char[]> toAdd = new List<char[]>();
            string currentID = string.Empty;
            foreach (var s in lines)
            {
                if (s.Contains(" "))
                {
                    var splits = s.Split(' ');
                    currentID = splits[1].Remove(splits[1].Length - 1, 1).Trim();
                    map.Add(currentID, new List<List<char[]>>());
                    names.Add(currentID);
                }
                else
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        toAdd.Add(s.ToArray());
                    }
                    else
                    {
                        map[currentID].Add(toAdd);

                        for (int i = 0; i < 3; i++)
                        {
                            var last = map[currentID].Last();

                            List<char[]> newArray = new List<char[]>();
                            for (int j = 0; j < last.Count; j++)
                            {
                                char[] arr = new char[last[0].Length];
                                newArray.Add(arr);
                            }

                            for (int k = 0; k < last.Count; k++)
                            {
                                for (int x = 0; x < last[0].Length; x++)
                                {
                                    newArray[x][newArray.Count - 1 - k] = last[k][x];
                                }
                            }

                            map[currentID].Add(newArray);
                        }

                        //Flip 180
                        List<char[]> flip = new List<char[]>();
                        for (int i = toAdd.Count - 1; i >= 0; i--)
                        {
                            flip.Add(toAdd[i]);
                        }
                        map[currentID].Add(flip);

                        for (int i = 0; i < 3; i++)
                        {
                            var last = map[currentID].Last();

                            List<char[]> newArray = new List<char[]>();
                            for (int j = 0; j < last.Count; j++)
                            {
                                char[] arr = new char[last[0].Length];
                                newArray.Add(arr);
                            }

                            for (int k = 0; k < last.Count; k++)
                            {
                                for (int x = 0; x < last[0].Length; x++)
                                {
                                    newArray[x][newArray.Count - 1 - k] = last[k][x];
                                }
                            }

                            map[currentID].Add(newArray);
                        }

                        currentID = string.Empty;
                        toAdd = new List<char[]>();
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day20();
        }
    }
}
