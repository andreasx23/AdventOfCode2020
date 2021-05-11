using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day07
{
    public class Day7Part2
    {
        private readonly Dictionary<string, TreeNode> map = new Dictionary<string, TreeNode>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var node in map.Values.OrderByDescending(n => n.Id))
            {
                node.CalculateTotalSubTreeSum();
            }

            List<TreeNode> nodes = map.Values.Where(n => n.Childs.Count > 0).OrderByDescending(n => n.Id).ToList();

            int ans = 0;
            foreach (var node in nodes)
            {
                long min = node.Childs.Min(n => n.TotalSubTreeSum);
                long max = node.Childs.Max(n => n.TotalSubTreeSum);
                int difference = (int)(max - min);

                if (difference == 0) continue;

                int minCount = node.Childs.Count(n => n.TotalSubTreeSum == min);
                int maxCount = node.Childs.Count(n => n.TotalSubTreeSum == max);

                TreeNode child = (maxCount >= minCount) ? node.Childs.First(n => n.TotalSubTreeSum == min) : node.Childs.First(n => n.TotalSubTreeSum == max);
                int prevWeight = child.Weight;
                child.Weight = (maxCount >= minCount) ? child.Weight + difference : child.Weight - difference;
                
                if (IsTreeBalanced(nodes))
                {
                    ans = child.Weight;
                    break;
                }

                child.Weight = prevWeight;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsTreeBalanced(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                node.CalculateTotalSubTreeSum();
                long min = node.Childs.Min(n => n.TotalSubTreeSum);
                long max = node.Childs.Max(n => n.TotalSubTreeSum);
                long difference = max - min;

                if (difference != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2017\Day07\input.txt";
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

                var actual = map[split.First()];
                for (int i = 3; i < split.Length; i++)
                {
                    var temp = split[i];
                    if (temp.Last() == ',')
                    {
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                    var current = map[temp];
                    current.Parent = actual;
                    actual.Childs.Add(current);
                }
            }

            var root = map.Values.First(n => n.Parent == null);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int id = 1;
            while (queue.Any())
            {
                var current = queue.Dequeue();
                current.Id = id++;

                foreach (var child in current.Childs)
                {
                    queue.Enqueue(child);
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
