using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day24
{
    public class Day24Part1
    {
        class Group
        {
            public bool IsImmuneSystem { get; set; }
            public int Units { get; set; }
            public int Hitpoints { get; set; }
            public int AttackDamage { get; set; }
            public string AttackStyle { get; set; }
            public int Initiative { get; set; }
            public List<string> Weaknesses { get; set; }
            public List<string> Immunities { get; set; }
            public int Id { get; set; }
            public int EffectivePower => Units * AttackDamage;
            public string Name => IsImmuneSystem ? $"Immune System group {Id}" : $"Infection group {Id}";
            public bool IsChosen { get; set; }
            public bool IsAlive => Units > 0;

            public int CalculateDamage(Group enemy)
            {
                if (enemy.Immunities != null && enemy.Immunities.Contains(AttackStyle))
                    return 0;
                else if (enemy.Weaknesses != null && enemy.Weaknesses.Contains(AttackStyle))
                    return EffectivePower * 2;
                else
                    return EffectivePower;
            }
        }

        private readonly List<Group> _groups = new List<Group>();

        private void Day24()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int rounds = 0;
            while (_groups.Any(g => g.IsImmuneSystem && g.IsAlive) && _groups.Any(g => !g.IsImmuneSystem && g.IsAlive))
            {
                //Console.WriteLine($"Round: {rounds}");
                var groups = _groups.OrderByDescending(g => g.EffectivePower);
                var maxEffectivePower = _groups.Max(g => g.EffectivePower);
                if (groups.Where(g => g.EffectivePower == maxEffectivePower).Count() > 1) groups = groups.OrderByDescending(g => g.Initiative);

                List<(Group attacker, Group enemy)> attackingPhase = new List<(Group attacker, Group enemy)>();
                foreach (var group in groups)
                {
                    var enemies = _groups.Where(g => group.IsImmuneSystem != g.IsImmuneSystem && !g.IsChosen && g.Immunities != null && !g.Immunities.Contains(group.AttackStyle));
                    if (enemies.Count() == 0) continue;
                    var maxDamage = enemies.Max(g => group.CalculateDamage(g));
                    //Console.WriteLine(group.Name + " " + maxDamage);
                    enemies = enemies.Where(g => group.CalculateDamage(g) == maxDamage);
                    if (enemies.Count() > 1)
                    {
                        enemies = enemies.OrderByDescending(g => g.EffectivePower);
                        maxEffectivePower = enemies.Max(g => g.EffectivePower);
                        enemies = enemies.Where(g => g.EffectivePower == maxEffectivePower);
                        if (enemies.Count() > 1) enemies = enemies.OrderByDescending(g => g.Initiative);
                    }
                    var enemy = enemies.FirstOrDefault();
                    if (enemy == null) continue;
                    //Console.WriteLine($"{group.Name} is attacking {enemy.Name} for a total of {maxDamage} damage");
                    enemy.IsChosen = true;
                    attackingPhase.Add((group, enemy));
                }

                foreach (var (attacker, enemy) in attackingPhase.OrderByDescending(g => g.attacker.Initiative))
                {
                    if (attacker.IsAlive)
                    {
                        var damage = attacker.CalculateDamage(enemy);
                        int killed = (int)Math.Floor((decimal)damage / enemy.Hitpoints);
                        enemy.Units -= killed;
                        Console.WriteLine($"{attacker.Name} is attacking {enemy.Name} for a total of {damage} damage killing a total of {killed} enemies");
                    }                    
                }
                //return;

                foreach (var group in _groups)
                {
                    group.IsChosen = false;
                }

                _groups.RemoveAll(g => !g.IsAlive);
                rounds++;
            }

            int ans = _groups.Sum(g => g.Units);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\Andreas\Desktop\AdventOfCode2020\2018\Day24\input.txt";
            var lines = File.ReadAllLines(path);

            bool isImmuneSystem = true;
            int id = 1;
            int index = 1;
            while (!string.IsNullOrWhiteSpace(lines[index]))
            {
                GenerateGroup(lines, isImmuneSystem, ref id, index);
                index++;
            }

            isImmuneSystem = false;
            id = 1;
            index += 2;
            for (int i = index; i < lines.Length; i++)
            {
                GenerateGroup(lines, isImmuneSystem, ref id, i);
            }
        }

        private void GenerateGroup(string[] lines, bool isImmuneSystem, ref int id, int index)
        {
            var s = lines[index];
            int left = s.IndexOf("("), right = s.IndexOf(")");

            if (left != -1)
            {
                //510 units each with 7363 hit points (weak to fire, radiation) with an attack that does 143 fire damage at initiative 5
                var leftSplit = s.Substring(0, left).Split(new string[] { "with" }, StringSplitOptions.RemoveEmptyEntries).Select(_s => _s.Trim());
                int units = int.Parse(new string(leftSplit.First().TakeWhile(c => char.IsDigit(c)).ToArray()));
                int hitpoints = int.Parse(new string(leftSplit.Last().TakeWhile(c => char.IsDigit(c)).ToArray()));

                var rightPart = s.Substring(right).Split(new string[] { "damage" }, StringSplitOptions.RemoveEmptyEntries).Select(_s => _s.Trim());
                var temp = rightPart.First().SkipWhile(c => !char.IsDigit(c));
                var damage = int.Parse(new string(temp.TakeWhile(c => char.IsDigit(c)).ToArray()));

                var damageStr = damage.ToString();
                var indexOf = rightPart.First().IndexOf(damageStr) + damageStr.Length + 1;
                var attackStyle = rightPart.First().Substring(indexOf).Trim().ToLower();

                var temp2 = rightPart.Last().SkipWhile(c => !char.IsDigit(c));
                var initiative = int.Parse(new string(temp2.TakeWhile(c => char.IsDigit(c)).ToArray()));

                var split = s.Substring(left + 1, right - left - 1).Split(';').Select(_s => _s.Trim());
                List<string> immunities = new List<string>();
                List<string> weaknesses = new List<string>();
                foreach (var item in split)
                {
                    if (item.Contains("immune"))
                    {
                        var removal = item.Replace("immune to ", "");
                        immunities = removal.Split(',').Select(val => val.Trim().ToLower()).ToList();
                    }
                    else
                    {
                        var removal = item.Replace("weak to ", "");
                        weaknesses = removal.Split(',').Select(val => val.Trim().ToLower()).ToList();
                    }
                }

                Group group = new Group()
                {
                    Id = id++,
                    Hitpoints = hitpoints,
                    Immunities = immunities,
                    Weaknesses = weaknesses,
                    AttackDamage = damage,
                    Units = units,
                    AttackStyle = attackStyle,
                    IsImmuneSystem = isImmuneSystem,
                    Initiative = initiative
                };
                _groups.Add(group);
            }
            else
            {
                //1950 units each with 8201 hit points with an attack that does 39 fire damage at initiative 15
                var units = int.Parse(new string(s.TakeWhile(c => char.IsDigit(c)).ToArray()));
                var nextPart = s.SkipWhile(c => char.IsDigit(c)).SkipWhile(c => !char.IsDigit(c));
                var hitpoints = int.Parse(new string(nextPart.TakeWhile(c => char.IsDigit(c)).ToArray()));
                nextPart = nextPart.SkipWhile(c => char.IsDigit(c)).SkipWhile(c => !char.IsDigit(c));
                var damage = int.Parse(new string(nextPart.TakeWhile(c => char.IsDigit(c)).ToArray()));
                var split = s.Split(' ').ToList();
                var indexOfDamage = split.IndexOf(damage.ToString());
                var attackStyle = split[indexOfDamage + 1].Trim();
                nextPart = nextPart.SkipWhile(c => char.IsDigit(c)).SkipWhile(c => !char.IsDigit(c));
                var initiative = int.Parse(new string(nextPart.TakeWhile(c => char.IsDigit(c)).ToArray()));

                Group group = new Group()
                {
                    Id = id++,
                    Hitpoints = hitpoints,
                    AttackDamage = damage,
                    Units = units,
                    AttackStyle = attackStyle,
                    IsImmuneSystem = isImmuneSystem,
                    Initiative = initiative
                };
                _groups.Add(group);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day24();
        }
    }
}
