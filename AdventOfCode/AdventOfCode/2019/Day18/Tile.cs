using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day18
{
    class Tile
    {
        public int X;
        public int Y;
        public int Cost;
        public int Distance;
        public int CostDistance => Cost + Distance;
    }
}
