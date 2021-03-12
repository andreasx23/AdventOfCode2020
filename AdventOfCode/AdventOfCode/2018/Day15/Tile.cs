using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day15
{
    public class Tile : IComparable<Tile>
    {
        public int ID;
        public int X;
        public int Y;
        public char Value;
        public int Attack;
        public int HP;
        public double priority; // smaller values are higher priority

        //A*
        public int Cost;
        public int Distance;
        public int CostDistance => Cost + Distance;
        public Tile Parent;

        public Tile()
        {
            Attack = 3;
            HP = 200;
        }

        public Tile(double priority)
        {
            this.priority = priority;
        }

        public int CompareTo(Tile other)
        {
            if (this.priority < other.priority) return -1;
            else if (this.priority > other.priority) return 1;
            else return 0;
        }
    }
}
