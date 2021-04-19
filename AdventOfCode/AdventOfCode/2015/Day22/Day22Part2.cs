using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day22
{
    public class Day22Part2
    {
        private readonly Boss boss = new Boss();
        private readonly Player player = new Player() { Hp = 50, Mana = 500 };

        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Queue<Game> queue = new Queue<Game>();
            foreach (var spell in player.Spells)
            {
                Player newPlayerState = new Player()
                {
                    Hp = player.Hp - 1,
                    Mana = player.Mana - spell.ManaCost,
                    TotalManaUsed = spell.ManaCost,
                    Spells = player.Spells.Select(s => (Spell)s.Clone()).ToList()
                };
                Boss newBossState = new Boss() { Hp = boss.Hp, Damage = boss.Damage };

                if (spell.HasEffect)
                {
                    newPlayerState.Spells.FirstOrDefault(s => s.Name == spell.Name).Activate();
                }
                else
                {
                    if (spell.Name == SpellName.MagicMissile)
                    {
                        newBossState.Hp -= spell.Damage;
                    }
                    else if (spell.Name == SpellName.Drain)
                    {
                        newPlayerState.Hp += spell.Heal;
                        newBossState.Hp -= spell.Damage;
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }

                Game newState = new Game(newPlayerState, newBossState);
                queue.Enqueue(newState);
            }

            int ans = 0;
            while (queue.Any())
            {
                Game current = queue.Dequeue();

                if (current.GameOver)
                {
                    if (current.IsPlayerWinner)
                    {
                        ans = current.player.TotalManaUsed;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                bool isPlayersTurn = true;
                for (int i = 0; i < 2; i++)
                {
                    if (isPlayersTurn) 
                    {
                        current.player.Hp -= 1;
                        if (current.GameOver) break; //Player loses
                    }

                    int playerDamage = 0;
                    int bossDamage = current.boss.Damage;
                    foreach (var spell in current.player.Spells.Where(s => s.IsEffectActive))
                    {
                        if (spell.Name == SpellName.Shield)
                        {
                            int armour = spell.EffectValue;
                            bossDamage = Math.Max(1, Math.Abs(armour - current.boss.Damage));
                        }
                        else if (spell.Name == SpellName.Poison)
                        {
                            int damage = spell.EffectValue;
                            playerDamage = damage;
                        }
                        else if (spell.Name == SpellName.Recharge)
                        {
                            int manaRegen = spell.EffectValue;
                            current.player.Mana += manaRegen;
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }

                        spell.EffectTurnsLeft--;
                        if (spell.EffectTurnsLeft <= 0)
                        {
                            spell.Deactivate();
                        }
                    }

                    if (isPlayersTurn)
                    {
                        isPlayersTurn = false;
                        current.boss.Hp -= playerDamage;
                    }
                    else
                    {
                        current.boss.Hp -= playerDamage;
                        current.player.Hp -= bossDamage;
                    }

                    if (current.GameOver) break;
                }

                if (current.GameOver)
                {
                    if (current.IsPlayerWinner)
                    {
                        ans = current.player.TotalManaUsed;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (var spell in current.player.Spells)
                {
                    if (current.player.Mana >= spell.ManaCost)
                    {
                        Player newPlayerState = new Player()
                        {
                            Hp = current.player.Hp,
                            Mana = current.player.Mana - spell.ManaCost,
                            TotalManaUsed = current.player.TotalManaUsed + spell.ManaCost,
                            Spells = current.player.Spells.Select(s => (Spell)s.Clone()).ToList()
                        };
                        Boss newBossState = new Boss() { Hp = current.boss.Hp, Damage = current.boss.Damage };

                        if (spell.HasEffect)
                        {
                            if (!spell.IsEffectActive)
                            {
                                newPlayerState.Spells.FirstOrDefault(s => s.Name == spell.Name).Activate();
                            }
                        }
                        else
                        {
                            if (spell.Name == SpellName.MagicMissile)
                            {
                                newBossState.Hp -= spell.Damage;
                            }
                            else if (spell.Name == SpellName.Drain)
                            {
                                newPlayerState.Hp += spell.Heal;
                                newBossState.Hp -= spell.Damage;
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }

                        Game newState = new Game(newPlayerState, newBossState);
                        queue.Enqueue(newState);
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2015\Day22\input.txt";
            var lines = File.ReadAllLines(path);
            bool isHp = true;
            foreach (var s in lines)
            {
                var index = s.LastIndexOf(' ');
                var num = int.Parse(s.Substring(index + 1));
                if (isHp)
                {
                    boss.Hp = num;
                    isHp = false;
                }
                else
                {
                    boss.Damage = num;
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day22();
        }
    }
}
