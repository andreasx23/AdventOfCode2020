using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day24
{
    public class Component
    {
        public int Left;
        public int Right;
        public int Score => Left + Right;
        public bool IsLeftUsed;
        public bool IsRightUsed;
    }
}
