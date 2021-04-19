using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day22
{
    public class Player
    {
        public int Mana;
        public int Hp;
        public List<Spell> Spells;
        public int TotalManaUsed;

        public Player()
        {
            Spells = new List<Spell>()
            {
                new Spell()
                {
                    Name = SpellName.MagicMissile,
                    ManaCost = 53,
                    Damage = 4
                },
                new Spell()
                {
                    Name = SpellName.Drain,
                    ManaCost = 73,
                    Damage = 2,
                    Heal = 2
                },
                new Spell()
                {
                    Name = SpellName.Shield,
                    ManaCost = 113,
                    HasEffect = true,
                    TotalEffectTurns = 6,
                    EffectValue = 7
                },
                new Spell()
                {
                    Name = SpellName.Poison,
                    ManaCost = 173,
                    HasEffect = true,
                    TotalEffectTurns = 6,
                    EffectValue = 3
                },
                new Spell()
                {
                    Name = SpellName.Recharge,
                    ManaCost = 229,
                    HasEffect = true,
                    TotalEffectTurns = 5,
                    EffectValue = 101
                },
            };
        }
    }
}
