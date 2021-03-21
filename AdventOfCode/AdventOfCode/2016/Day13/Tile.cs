using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day13
{
    public class Tile
    {
        public int X;
        public int Y;
        public int Cost;
        public int Distance;
        public int CostDistance => Cost * Distance;
        public Tile Parent;

        public void CalculateManhattenDistance(int x2, int y2)
        {
            Distance = Math.Abs(X - x2) + Math.Abs(Y - y2);
        }
    }
}
