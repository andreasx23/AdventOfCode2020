using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day07
{
    public class Day7Part1
    {
        private readonly Dictionary<string, TreeNode> map = new Dictionary<string, TreeNode>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string ans = map.Values.First(t => t.Parent == null).Name;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2017\Day07\input.txt";
            var lines = File.ReadAllLines(path);

            List<string> children = new List<string>();
            foreach (var s in lines)
            {
                var split = s.Split(' ').ToArray();

                if (split.Count() > 2)
                {
                    children.Add(s);
                }

                TreeNode node = new TreeNode()
                {
                    Name = split.First(),
                    Weight = int.Parse(split[1].Substring(1, split[1].Length - 2))
                };
                map.Add(node.Name, node);
            }

            foreach (var s in children)
            {
                var split = s.Split(' ').ToArray();

                var node = map[split.First()];
                for (int i = 3; i < split.Length; i++)
                {
                    var temp = split[i];
                    if (temp.Last() == ',')
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                    var child = map[temp];
                    child.Parent = node;
                    node.Childs.Add(child);
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
