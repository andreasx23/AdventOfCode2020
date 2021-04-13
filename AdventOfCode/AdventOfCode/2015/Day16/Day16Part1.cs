using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day16
{
    public class Day16Part1
    {
        private List<Aunt> input = new List<Aunt>();
        private readonly Aunt target = new Aunt()
        {
            Id = -1,
            Children = 3,
            Cats = 7,
            SamoyedsDog = 2,
            PomeraniansDog = 3,
            AkitasDog = 0,
            VizslasDog = 0,
            Goldfish = 5,
            Trees = 3,
            Cars = 2,
            Perfumes = 1
        };

        private void Day16()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            input = input.Where(a => a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Children == target.Children ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Cats == target.Cats ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.SamoyedsDog == target.SamoyedsDog ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.PomeraniansDog == target.PomeraniansDog ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Goldfish == target.Goldfish ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Trees == target.Trees ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Cars == target.Cars ||
                    a.AkitasDog == target.AkitasDog && a.VizslasDog == target.VizslasDog && a.Perfumes == target.Perfumes).ToList();

            int ans = input.First().Id;

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\2015\Day16\input.txt";
            string[] lines = File.ReadAllLines(path);

            int id = 1;
            foreach (var s in lines)
            {
                string sub = s.Substring(6 + id.ToString().Length);
                Aunt sue = new Aunt()
                {
                    Id = id
                };

                IEnumerable<string> splits = sub.Split(',').Select(s => s.Trim().ToLower());
                foreach (var split in splits)
                {
                    if (split.Contains("children"))
                    {
                        var word = "children: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Children = amount;
                    }
                    else if (split.Contains("cats"))
                    {
                        var word = "cats: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Cats = amount;
                    }
                    else if (split.Contains("samoyeds"))
                    {
                        var word = "samoyeds: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.SamoyedsDog = amount;
                    }
                    else if (split.Contains("pomeranians"))
                    {
                        var word = "pomeranians: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.PomeraniansDog = amount;
                    }
                    else if (split.Contains("akitas"))
                    {
                        var word = "akitas: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.AkitasDog = amount;
                    }
                    else if (split.Contains("vizslas"))
                    {
                        var word = "vizslas: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.VizslasDog = amount;
                    }
                    else if (split.Contains("goldfish"))
                    {
                        var word = "goldfish: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Goldfish = amount;
                    }
                    else if (split.Contains("trees"))
                    {
                        var word = "trees: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Trees = amount;
                    }
                    else if (split.Contains("cars"))
                    {
                        var word = "cars: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Cars = amount;
                    }
                    else if (split.Contains("perfumes"))
                    {
                        var word = "perfumes: ";
                        var amount = int.Parse(split.Replace(word, ""));
                        sue.Perfumes = amount;
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                        Console.WriteLine(split);
                        break;
                    }
                }
                id++;
                input.Add(sue);
            }
        }

        public void TestCase()
        {
            ReadData();
            Day16();
        }
    }
}
