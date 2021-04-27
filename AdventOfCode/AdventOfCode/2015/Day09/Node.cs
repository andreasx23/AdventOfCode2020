using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day09
{
    public class Node
    {
        public string Name;
        public List<(Node neighbour, int distance)> Edges;

        public Node()
        {
            Edges = new List<(Node neighbour, int distance)>();
        }
    }
}
