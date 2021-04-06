using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day21
{
    public class Day21Part2
    {
        private readonly List<List<Entity>> gears = new List<List<Entity>>();

        private void Day21()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int ans = int.MinValue;
            for (int i = 0; i < gears[0].Count; i++)
            {
                for (int j = 0; j < gears[1].Count; j++)
                {
                    for (int k = 0; k < gears[2].Count; k++)
                    {
                        for (int x = k + 1; x < gears[2].Count; x++)
                        {
                            Npc player = new Npc
                            {
                                Hp = 100,
                                Damange = gears[0][i].Damage + gears[1][j].Damage + gears[2][k].Damage + gears[2][x].Damage,
                                Armour = gears[0][i].Armour + gears[1][j].Armour + gears[2][k].Armour + gears[2][x].Armour
                            };
                            int cost = gears[0][i].Cost + gears[1][j].Cost + gears[2][k].Cost + gears[2][x].Cost;

                            if (!Battle(player, Boss())) ans = Math.Max(ans, cost);
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private bool Battle(Npc player, Npc boss)
        {
            int playerDamagePrTurn = Math.Max(player.Damange - boss.Armour, 1);
            int bossDamagePrTurn = Math.Max(boss.Damange - player.Armour, 1);

            while (player.Hp > 0 && boss.Hp > 0)
            {
                player.Hp -= bossDamagePrTurn;
                boss.Hp -= playerDamagePrTurn;
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
                    gears.Add(currentGear);
                    currentGear = new List<Entity>();
                    if (currentType == Type.Weapon)
                    {
                        currentType = Type.Armour;
                        currentGear.Add(new Entity()
                        {
                            Cost = 0,
                            Armour = 0,
                            Damage = 0,
                            Name = "No armour",
                            Type = currentType
                        });
                    }
                    else
                    {
                        currentType = Type.Ring;
                        currentGear.Add(new Entity()
                        {
                            Cost = 0,
                            Armour = 0,
                            Damage = 0,
                            Name = "No ring 1",
                            Type = currentType
                        });

                        currentGear.Add(new Entity()
                        {
                            Cost = 0,
                            Armour = 0,
                            Damage = 0,
                            Name = "No ring 2",
                            Type = currentType
                        });
                    }
                    i++;                    
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
