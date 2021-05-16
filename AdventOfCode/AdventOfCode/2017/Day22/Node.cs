using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day22
{
    public class Node
    {
        public bool IsInfected;
        public State State;
    }

    public enum State
    {
        CLEAN,
        WEAKENED,
        INFECTED,
        FLAGGED
    }

    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}
