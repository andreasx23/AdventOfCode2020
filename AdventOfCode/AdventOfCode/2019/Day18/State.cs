using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day18
{
    class State
    {
        public int Steps;
        public HashSet<char> CollectedKeys = new HashSet<char>();
        public char Target;
        public int KeyCount;
    }
}
