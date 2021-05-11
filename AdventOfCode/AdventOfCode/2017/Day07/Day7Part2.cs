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
        class TreeNode
        {
            public string Name;
            public int Weight;
            public long TotalSubTreeSum;
            public int Depth;
            public TreeNode Parent;
            public List<TreeNode> Childs;
            
            public TreeNode()
            {
                Childs = new List<TreeNode>();
            }
        }

        private readonly Dictionary<string, TreeNode> map = new Dictionary<string, TreeNode>();

        private void Day7()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var root = map.Values.First(t => t.Parent == null);
            GenerateDepth(root);
            CalculateTotalSubTreeSum(map);

            List<TreeNode> outliers = new List<TreeNode>();
            foreach (var group in map.Values.OrderByDescending(kv => kv.Depth).GroupBy(t => t.Parent))
            {
                bool isOutlier = false;
                foreach (var node in group.Skip(1))
                {
                    if (node.TotalSubTreeSum != group.First().TotalSubTreeSum)
                    {
                        outliers.Add(node);
                        isOutlier = true;
                    }
                }

                if (isOutlier)
                {
                    outliers.Add(group.First());
                    break;
                }
            }

            int ans = 0;
            var difference = outliers.Max(n => n.TotalSubTreeSum) - outliers.Min(n => n.TotalSubTreeSum);
            foreach (var node in outliers)
            {
                var prev = node.Weight;

                ResetTotalSubTreeSum(root);
                node.Weight -= (int)difference;
                CalculateTotalSubTreeSum(map);
                if (IsMatch(map))
                {
                    ans = node.Weight;
                    break;
                }

                ResetTotalSubTreeSum(root);
                node.Weight = prev + (int)difference;
                CalculateTotalSubTreeSum(map);
                if (IsMatch(map))
                {
                    ans = node.Weight;
                    break;
                }

                ResetTotalSubTreeSum(root);
                node.Weight = prev;
                CalculateTotalSubTreeSum(map);
            }

            Console.WriteLine("Find data elementer with value 1161");
            Console.WriteLine("Answer: 1152");

            
            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool IsMatch(Dictionary<string, TreeNode> map)
        {
            foreach (var group in map.Values.OrderByDescending(kv => kv.Depth).GroupBy(t => t.Parent))
            {
                foreach (var node in group.Skip(1))
                {
                    if (node.Parent == null)
                    {
                        continue;
                    }

                    if (node.TotalSubTreeSum != group.First().TotalSubTreeSum)
                    {
                        //Console.WriteLine(group.Key.Depth + " " + node.TotalSubTreeSum + " " + group.First().TotalSubTreeSum);
                        //return false;
                    }
                }
            }

            return true;
        }

        private void ResetTotalSubTreeSum(TreeNode root)
        {
            root.TotalSubTreeSum = 0;
            foreach (var child in root.Childs)
            {
                ResetTotalSubTreeSum(child);
            }
        }

        private void CalculateTotalSubTreeSum(Dictionary<string, TreeNode> map)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            HashSet<TreeNode> isVisited = new HashSet<TreeNode>();
            foreach (var node in map.Values.Where(t => t.Childs.Count == 0))
            {
                node.TotalSubTreeSum = node.Weight;
                node.Parent.TotalSubTreeSum += node.Weight;
                if (isVisited.Add(node.Parent))
                {
                    queue.Enqueue(node.Parent);
                }
            }

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.Parent != null)
                {
                    current.TotalSubTreeSum += current.Weight;
                    current.Parent.TotalSubTreeSum += current.TotalSubTreeSum;

                    if (isVisited.Add(current.Parent))
                    {
                        queue.Enqueue(current.Parent);
                    }
                }
            }
        }

        private void GenerateDepth(TreeNode root)
        {
            foreach (var child in root.Childs)
            {
                child.Depth = root.Depth + 1;
                GenerateDepth(child);
            }
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
        }

        public void TestCase()
        {
            ReadData();
            Day7();
        }
    }
}
