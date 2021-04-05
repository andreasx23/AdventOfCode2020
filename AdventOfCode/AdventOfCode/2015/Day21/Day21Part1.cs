using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day21
{
    public class Day21Part1
    {
        class Npc
        {
            public int Hp;
            public int Damange;
            public int Armour;
        }

        class Entity
        {
            public string Name;
            public int Cost;
            public int Damage;
            public int Armour;
            public Type Type;
        }

        enum Type
        {
            Weapon = 1,
            Armour = 2,
            Ring = 3
        }

        private readonly List<List<Entity>> gears = new List<List<Entity>>();

        private void Day21()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            int ans = int.MaxValue;
            for (int i = 0; i < gears[0].Count; i++) //Must buy 1
            {                              
                for (int j = 0; j < gears[1].Count; j++) //Optional
                {
                    for (int k = 0; k < gears[2].Count; k++) //Can buy upto 2 and minimum 0
                    {
                        Npc player = new Npc
                        {
                            Hp = 100,
                            Damange = gears[0][i].Damage + gears[1][j].Damage + gears[2][k].Damage,
                            Armour = gears[0][i].Armour + gears[1][j].Armour + gears[2][k].Armour
                        };
                        int currentCost = gears[0][i].Cost + gears[1][j].Cost + gears[2][k].Cost;

                        if (Battle(player, Boss()))
                        {
                            ans = Math.Min(ans, currentCost);
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool Battle(Npc player, Npc boss)
        {
            int playerDamagePrTurn = player.Damange - boss.Armour;
            int bossDamagePrTurn = boss.Damange - player.Armour;

            if (playerDamagePrTurn <= 0)
            {
                playerDamagePrTurn = 1;
            }

            if (bossDamagePrTurn <= 0)
            {
                bossDamagePrTurn = 1;
            }

            bool IsPlayer = true;
            while (player.Hp > 0 && boss.Hp > 0)
            {
                if (IsPlayer)
                {
                    boss.Hp -= playerDamagePrTurn;
                }
                else
                {
                    player.Hp -= bossDamagePrTurn;
                }

                IsPlayer = !IsPlayer;
            }

            return player.Hp > 0;
        }

        private Npc Boss()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day21\input.txt";
            var bossStats = File.ReadAllLines(path).Select(s => int.Parse(s.Split(' ').Last())).ToList();
            Npc boss = new Npc()
            {
                Hp = bossStats[0],
                Damange = bossStats[1],
                Armour = bossStats[2]
            };
            return boss;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day21\Gear.txt";
            var gearInformation = File.ReadAllLines(path);
            Type currentType = Type.Weapon;
            List<Entity> currentGear = new List<Entity>();
            for (int i = 1; i < gearInformation.Length; i++)
            {
                string s = gearInformation[i];
                if (!string.IsNullOrEmpty(s))
                {
                    var details = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (currentType != Type.Ring)
                    {
                        currentGear.Add(new Entity()
                        {
                            Type = currentType,
                            Name = details[0],
                            Cost = int.Parse(details[1]),
                            Damage = int.Parse(details[2]),
                            Armour = int.Parse(details[3])
                        });
                    }
                    else
                    {
                        currentGear.Add(new Entity()
                        {
                            Type = currentType,
                            Name = details[0] + " " + details[1],
                            Cost = int.Parse(details[2]),
                            Damage = int.Parse(details[3]),
                            Armour = int.Parse(details[4])
                        });
                    }
                }
                else
                {
                    if (currentType == Type.Weapon)
                    {
                        currentType = Type.Armour;
                    }
                    else
                    {
                        currentType = Type.Ring;
                    }
                    i++;
                    gears.Add(currentGear);
                    currentGear = new List<Entity>();
                }
            }
            gears.Add(currentGear);
        }

        public void TestCase()
        {
            ReadData();
            Day21();
        }
    }
}
