using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day25
{
    public class Day25Part1
    {
        class State
        {
            public string Name;
            public int ZeroWrite;
            public int ZeroMove;
            public string ZeroNextState;
            public int OneWrite;
            public int OneMove;
            public string OneNextState;

            public (string next, int value) ExecuteStep(int value, ref int index)
            {
                if (value == 0)
                {
                    index += ZeroMove;
                    return (ZeroNextState, ZeroWrite);
                }
                else
                {
                    index += OneMove;
                    return (OneNextState, OneWrite);
                }
            }
        }

        private readonly Dictionary<string, State> map = new Dictionary<string, State>();
        private string firstState;
        private int totalMoves;

        private void Day25()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Dictionary<int, int> array = new Dictionary<int, int>();
            int index = 0;
            var current = map[firstState];
            for (int i = 0; i < totalMoves; i++)
            {
                if (!array.ContainsKey(index)) array.Add(index, 0);

                int prevIndex = index;
                (string next, int value) = current.ExecuteStep(array[index], ref index);
                array[prevIndex] = value;
                current = map[next];
            }

            int ans = array.Values.Count(v => v == 1);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private string RemoveDot(string s)
        {
            return s.Substring(0, s.Length - 1);
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day25\input.txt";
            var lines = File.ReadAllLines(path);

            firstState = RemoveDot(lines.First()).Split(' ').Last();
            totalMoves = int.Parse(lines[1].Split(' ')[5]);

            for (int i = 3; i < lines.Length; i += 10)
            {
                var state = RemoveDot(lines[i]).Split(' ').Last();
                var zeroWrite = int.Parse(RemoveDot(lines[i + 2]).Split(' ').Last());
                var zeroMove = lines[i + 3].Contains("right") ? 1 : -1;
                var zeroNextState = RemoveDot(lines[i + 4]).Split(' ').Last();

                var oneWrite = int.Parse(RemoveDot(lines[i + 6]).Split(' ').Last());
                var oneMove = lines[i + 7].Contains("right") ? 1 : -1;
                var oneNextState = RemoveDot(lines[i + 8]).Split(' ').Last();

                map.Add(state, new State()
                {
                    Name = state,
                    ZeroWrite = zeroWrite,
                    ZeroMove = zeroMove,
                    ZeroNextState = zeroNextState,
                    OneWrite = oneWrite,
                    OneMove = oneMove,
                    OneNextState = oneNextState
                });
            }
        }

        public void TestCase()
        {
            ReadData();
            Day25();
        }
    }
}
