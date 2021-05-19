using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019.Day06
{
    public class Planet
    {
        public string Name;
        public Planet Parent;
        public List<Planet> Children;

        public Planet()
        {
            Children = new List<Planet>();
        }
    }
}
