using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day21
{
    public class Day21Part2
    {
        private readonly Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        private readonly List<string> ingredients = new List<string>();
        private readonly List<string> allergens = new List<string>();

        /*
         * Apparantly my codes didn't think sesame had any allergen, but soy had two (snmxl bd) so after 
         * some trail and error I found out that sesame's allergen was snmxl and my answer was
         * hkflr,ctmcqjf,bfrq,srxphcm,snmxl,zvx,bd,mqvk
         * 
         * Code produces answer: hkflr,ctmcqjf,bfrq,srxphcm,zvx,snmxl,bd,mqvk
         * Actual answer: hkflr,ctmcqjf,bfrq,srxphcm,snmxl,zvx,bd,mqvk
         */
        private void Day21()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var orderedMap = map.OrderBy(kv => kv.Value.Count);
            foreach (var kvp in orderedMap)
            {
                if (kvp.Value.Count == 1)
                {
                    allergens.AddRange(kvp.Value);
                }
                else
                {
                    var filteredList = kvp.Value.Except(allergens).ToList();
                    allergens.AddRange(filteredList);
                    map[kvp.Key] = filteredList;
                }
            }

            orderedMap = map.OrderBy(kv => kv.Key);
            List<string> answers = new List<string>();
            foreach (var kvp in orderedMap)
            {
                //Console.WriteLine(kvp.Key + " " + kvp.Value.Count + " " + string.Join(" ", kvp.Value));
                answers.AddRange(kvp.Value);
            }
            var ans = string.Join(",", answers);

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 21\input.txt";
            var lines = File.ReadAllLines(path);

            foreach (var s in lines)
            {
                var split = s.Split(new string[] { " (contains" }, StringSplitOptions.None);
                var ingredients = split[0].Split(' ').Select(i => i.Trim()).ToList();
                var allergens = split[1].Split(',').Select(a => a.Trim().Replace(")", "")).ToList();

                this.ingredients.AddRange(ingredients);
                this.allergens.AddRange(allergens);

                foreach (var allergen in allergens)
                {
                    if (!map.ContainsKey(allergen))
                    {
                        map.Add(allergen, ingredients);
                    }
                    else
                    {
                        var intersect = map[allergen].Intersect(ingredients).ToList();
                        map[allergen] = intersect;
                    }
                }
            }
        }

        public void TestCase()
        {
            ReadData();
            Day21();
        }
    }
}
