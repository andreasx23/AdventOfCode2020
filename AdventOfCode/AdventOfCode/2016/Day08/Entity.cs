using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day08
{
    public class Entity
    {
        internal Type Type { get; set; }
        public int A;
        public int B;
        public int Amount;
        public int Row;
        public int Column;
    }

    enum Type
    {
        rect = 1,
        row = 2,
        column = 3
    }
}
