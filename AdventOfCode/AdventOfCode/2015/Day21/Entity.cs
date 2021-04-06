using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day21
{
    public class Entity
    {
        public string Name;
        public int Cost;
        public int Damage;
        public int Armour;
        public Type Type;
    }

    public enum Type
    {
        Weapon = 1,
        Armour = 2,
        Ring = 3
    }
}
