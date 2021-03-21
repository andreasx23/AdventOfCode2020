using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day10
{
    public class Bot
    {
        public int Id;
        public int LowOutput;
        public int HighOutput;
        public Bot LowBot;
        public Bot HighBot;
        public List<int> ValueChips;

        public Bot()
        {
            LowOutput = -1;
            HighOutput = -1;
            ValueChips = new List<int>();
        }

        public bool HasLowBot()
        {
            return LowBot != null;
        }

        public bool HasHighBot()
        {
            return HighBot != null;
        }
    }
}
