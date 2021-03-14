using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day15
{
    public class Unit : IComparable<Unit>
    {
        public int ID;
        public int X;
        public int Y;
        public char Type;
        public int Attack;
        public int HP;
        public bool isAlive;        

        //A*
        public int Cost;
        public int Distance;
        public int CostDistance => Cost + Distance;
        public Unit Parent;
        public double Priority; // smaller values are higher priority

        public Unit()
        {
            Attack = 3;
            HP = 200;
        }

        public Unit(double priority)
        {
            this.Priority = priority;
            //isAlive = true;
        }

        public int CompareTo(Unit other)
        {
            if (this.Priority < other.Priority) return -1;
            else if (this.Priority > other.Priority) return 1;
            else return 0;
        }
    }
}
