using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day13
{
    public class Person
    {
        public string Name;
        public List<(Person neighbour, int happiness)> Neighbours;

        public Person()
        {
            Neighbours = new List<(Person neighbour, int happiness)>();
        }
    }
}
