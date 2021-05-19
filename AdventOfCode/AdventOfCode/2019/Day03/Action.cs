using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day03
{
    public class Action
    {
        public Direction Direction;
        public int Steps;
    }

    public enum Direction
    {
        RIGHT = 'R',
        LEFT = 'L',
        UP = 'U',
        DOWN = 'D'
    }

    public enum Drawing
    {
        ROAD = '.',
        TURN = '+',
        INTERSECTION = 'X'
    }
}
