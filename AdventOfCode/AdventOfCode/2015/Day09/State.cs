using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day09
{
    public class State
    {
        public int Score;
        public HashSet<Node> Visited = new HashSet<Node>();
        public Node Node;
    }
}
