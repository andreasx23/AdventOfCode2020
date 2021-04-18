using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day22
{
    public class Day22Part1
    {
        class Boss
        {
            public int Hp;            
            public int Damage;            
        }

        class Player
        {
            public int Mana;
            public int Hp;
            public List<Spell> Spells;
            public int TotalManaUsed;

            public Player(int hp, int mana)
            {
                Spells = new List<Spell>()
                {
                    new Spell()
                    {
                        Name = "Magic Missile",
                        ManaCost = 53,
                        Damage = 4
                    },
                    new Spell()
                    {
                        Name = "Drain",
                        ManaCost = 73,
                        Damage = 2,
                        Heal = 2
                    },
                    new Spell()
                    {
                        Name = "Shield",
                        ManaCost = 113,
                        HasEffect = true,
                        TotalEffectTurns = 6,
                        EffectValue = 7
                    },
                    new Spell()
                    {
                        Name = "Poison",
                        ManaCost = 173,
                        HasEffect = true,
                        TotalEffectTurns = 6,
                        EffectValue = 3
                    },
                    new Spell()
                    {
                        Name = "Recharge",
                        ManaCost = 229,
                        HasEffect = true,
                        TotalEffectTurns = 5,
                        EffectValue = 101
                    },
                };
                Hp = hp;
                Mana = mana;
            }
        }

        class Spell
        {
            public string Name;
            public int ManaCost;
            public int Damage;
            public int Heal;
            public bool HasEffect;
            public bool IsEffectActive;
            public int TotalEffectTurns;
            public int EffectTurnsLeft;
            public int EffectValue;
        }

        private readonly Boss boss = new Boss();
        private readonly Player player = new Player(50, 500);

        class Game
        {
            public Player player;
            public Boss boss;
            public bool GameOver => player.Hp == 0 || boss.Hp == 0;
            public bool Winner => player.Hp > 0;

            public Game(Player player, Boss boss)
            {
                this.player = player;
                this.boss = boss;
            }
        }

        private void Day22()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Queue<Game> queue = new Queue<Game>();
            foreach (var spell in player.Spells)
            {
                Player newPlayerState = new Player(player.Hp, player.Mana)
                {
                    TotalManaUsed = spell.ManaCost,
                    Mana = player.Mana - spell.ManaCost,
                    Spells = player.Spells.ToList()
                };
                Boss newBossState = new Boss() { Hp = boss.Hp, Damage = boss.Damage };

                if (spell.HasEffect)
                {
                    newPlayerState.Spells.First(s => s.Name == spell.Name).IsEffectActive = true;
                }
                else
                {
                    if (spell.Name == "Magic Missile")
                    {
                        newBossState.Hp -= spell.Damage;
                    }
                    else
                    {
                        newPlayerState.Hp += spell.Heal;
                        newBossState.Hp -= spell.Damage;
                    }
                }

                Game newState = new Game(newPlayerState, newBossState);
                queue.Enqueue(newState);
            }

            int ans = int.MaxValue;
            while (queue.Any())
            {
                Game current = queue.Dequeue();

                if (current.GameOver)
                {
                    if (current.Winner)
                    {
                        ans = Math.Min(ans, current.player.TotalManaUsed);
                    }
                    continue;
                }

                bool isPlayer = true;
                for (int i = 0; i < 2; i++)
                {
                    int playerDamage = 0;
                    int bossDamage = current.boss.Damage;
                    foreach (var spell in current.player.Spells.Where(s => s.IsEffectActive))
                    {
                        if (spell.Name == "Shield")
                        {
                            int armour = spell.EffectValue;
                            bossDamage = Math.Max(1, Math.Abs(armour - current.boss.Damage));
                        }
                        else if (spell.Name == "Poison")
                        {
                            int damage = spell.EffectValue;
                            playerDamage = damage;
                        }
                        else
                        {
                            int manaRegen = spell.EffectValue;
                            current.player.Mana += manaRegen;
                        }

                        spell.EffectTurnsLeft--;
                        if (spell.EffectTurnsLeft <= 0)
                        {
                            spell.IsEffectActive = false;
                            spell.EffectTurnsLeft = spell.TotalEffectTurns;
                        }
                    }

                    if (isPlayer)
                    {
                        isPlayer = false;
                        current.boss.Hp -= playerDamage;
                    }
                    else
                    {
                        current.boss.Hp -= playerDamage;
                        current.player.Hp -= bossDamage;
                    }

                    if (current.GameOver)
                    {
                        if (current.Winner)
                        {
                            ans = Math.Min(ans, current.player.TotalManaUsed);
                        }
                        continue;
                    }
                }
                
                foreach (var spell in current.player.Spells)
                {
                    if (current.player.Mana >= spell.ManaCost)
                    {
                        Player newPlayerState = new Player(current.player.Hp, current.player.Mana)
                        {
                            TotalManaUsed = current.player.TotalManaUsed + spell.ManaCost,
                            Mana = current.player.Mana - spell.ManaCost,
                            Spells = current.player.Spells.ToList()
                        };
                        Boss newBossState = new Boss() { Hp = current.boss.Hp, Damage = current.boss.Damage };

                        if (spell.HasEffect)
                        {
                            if (!spell.IsEffectActive)
                            {
                                newPlayerState.Spells.FirstOrDefault(s => s.Name == spell.Name).IsEffectActive = true;
                            }
                        }
                        else
                        {
                            if (spell.Name == "Magic Missile")
                            {
                                newBossState.Hp -= spell.Damage;
                            }
                            else
                            {
                                newPlayerState.Hp += spell.Heal;
                                newBossState.Hp -= spell.Damage;
                            }
                        }

                        Game newState = new Game(newPlayerState, newBossState);
                        queue.Enqueue(newState);
                    }
                }
            }

            Console.WriteLine("Greater than 444");

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day22\input.txt";
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
