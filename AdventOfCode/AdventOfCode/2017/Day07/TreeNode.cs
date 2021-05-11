using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day07
{
    public class TreeNode
    {
        public int Id;
        public string Name;
        public int Weight;
        public long TotalSubTreeSum;
        public TreeNode Parent;
        public List<TreeNode> Childs = new List<TreeNode>();

        public void CalculateTotalSubTreeSum()
        {
            TotalSubTreeSum = Weight + Childs.Sum(c => c.TotalSubTreeSum);
        }
    }
}
