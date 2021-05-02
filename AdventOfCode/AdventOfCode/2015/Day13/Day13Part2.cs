using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day13
{
    public class Day13Part2
    {
        private readonly List<Person> persons = new List<Person>();
        private readonly List<List<Person>> tables = new List<List<Person>>();

        private void Day13()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Person me = new Person()
            {
                Name = "Me"
            };

            persons.Add(me);

            foreach (var person in persons)
            {
                me.Neighbours.Add((person, 0));
                person.Neighbours.Add((me, 0));
            }

            Permutation(persons, 0);

            int ans = int.MinValue;
            foreach (var table in tables)
            {
                int sum = 0;
                for (int i = 0; i < table.Count; i++)
                {
                    Person left = null, current = table[i], right = null;
                    if (i == 0)
                    {
                        left = table.Last();
                        right = table[i + 1];
                    }
                    else if (i + 1 == table.Count)
                    {
                        left = table[i - 1];
                        right = table.First();
                    }
                    else
                    {
                        left = table[i - 1];
                        right = table[i + 1];
                    }

                    int leftHappinessScore = current.Neighbours.FirstOrDefault(p => p.neighbour.Name == left.Name).happiness;
                    int rightHapinessScore = current.Neighbours.FirstOrDefault(p => p.neighbour.Name == right.Name).happiness;
                    sum += leftHappinessScore + rightHapinessScore;
                }

                ans = Math.Max(ans, sum);
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void Permutation(List<Person> values, int index)
        {
            if (values.Count == index)
            {
                tables.Add(new List<Person>(values));
            }
            else
            {
                for (int i = index; i < values.Count; i++)
                {
                    Swap(values, i, index);
                    Permutation(values, index + 1);
                    Swap(values, i, index);
                }
            }
        }

        private void Swap(List<Person> values, int pos1, int pos2)
        {
            var temp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = temp;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day13\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(' ').ToArray();
                string myName = split.First();
                string neighbourName = split.Last().Substring(0, split.Last().Length - 1);
                int happiness = int.Parse(split[3]);
                if (split[2] == "lose") happiness = -happiness;

                Person person = persons.FirstOrDefault(p => p.Name == myName);
                if (person == null)
                {
                    person = new Person()
                    {
                        Name = myName
                    };

                    persons.Add(person);
                }

                Person neighbour = persons.FirstOrDefault(p => p.Name == neighbourName);
                if (neighbour == null)
                {
                    neighbour = new Person()
                    {
                        Name = neighbourName
                    };

                    persons.Add(neighbour);
                }

                person.Neighbours.Add((neighbour, happiness));
            }
        }

        public void TestCase()
        {
            ReadData();
            Day13();
        }
    }
}
