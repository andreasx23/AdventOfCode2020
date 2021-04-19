using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day22
{
    public class Spell : ICloneable
    {
        public SpellName Name;
        public int ManaCost;
        public int Damage;
        public int Heal;
        public bool HasEffect;
        public bool IsEffectActive;
        public int TotalEffectTurns;
        public int EffectTurnsLeft;
        public int EffectValue;

        public object Clone()
        {
            return new Spell()
            {
                Damage = Damage,
                EffectTurnsLeft = EffectTurnsLeft,
                EffectValue = EffectValue,
                HasEffect = HasEffect,
                Heal = Heal,
                IsEffectActive = IsEffectActive,
                ManaCost = ManaCost,
                Name = Name,
                TotalEffectTurns = TotalEffectTurns
            };
        }

        public void Activate()
        {
            if (!HasEffect) return;

            IsEffectActive = true;
            EffectTurnsLeft = TotalEffectTurns;
        }

        public void Deactivate()
        {
            if (!HasEffect) return;

            IsEffectActive = false;
            EffectTurnsLeft = TotalEffectTurns;
        }
    }

    public enum SpellName
    {
        MagicMissile = 1,
        Drain = 2,
        Shield = 3,
        Poison = 4,
        Recharge = 5,
    }
}
