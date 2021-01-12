using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day06
{
    public class Day6Part1
    {
        private List<string> input = new List<string>();
        private int maxDepth = 0;

        public class TreeNode
        {
            public TreeNode left = null;
            public TreeNode right = null;
            public string value;
            public int depth;
        }

        //ans > 1517 && ans < 1142322
        private void Day6()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var baseCase = input[0].Split(')');
            TreeNode root = new TreeNode
            {
                value = baseCase.First(),
                right = new TreeNode() 
                { 
                    value = baseCase.Last(),
                    depth = 1 
                },
                depth = 0
            };

            int ans = 1;
            for (int i = 1; i < input.Count; i++)
            {
                var split = input[i].Split(')');
                string planet = split.First(), orbit = split.Last();

                var current = Find(root, planet, 0);
                var newNode = new TreeNode()
                {
                    value = orbit,
                    //depth = current.depth + 1
                };

                if (current.right == null)
                {
                    current.right = newNode;
                }
                else
                {
                    current.left = newNode;
                }

                ans += newNode.depth;
            }

            Print(root);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private TreeNode Find(TreeNode root, string key, int depth)
        {
            if (root == null)
            {
                return null;
            }

            if (key.Equals(root.value))
            {
                return root;
            }

            var left = Find(root.left, key, depth + 1);

            if (left != null) return left;

            var right = Find(root.right, key, depth + 1);

            return right;
        }

        private void Print(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine(root.value + " " + root.depth);
            Print(root.left);
            Print(root.right);
        }

        private void ReadData()
        {
            string path = @"C:\Users\bruger\Desktop\AdventOfCode2020\2019\Day06\input.txt";
            input = File.ReadAllLines(path).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day6();
        }
    }
}
