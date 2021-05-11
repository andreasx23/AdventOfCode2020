using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day12
{
    public class Pipe
    {
        public int Id { get; set; }
        public List<Pipe> Connections = new List<Pipe>();
    }
}
