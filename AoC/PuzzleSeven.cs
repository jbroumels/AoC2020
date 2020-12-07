using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleSeven : Puzzle
    {
        public PuzzleSeven() : base("07")
        {

        }

        public override object GetAnswerOne()
        {
            BagRule[] rules = GetRules().ToArray();

            BagRule[] bags = FindContainsRecursive(rules, "shiny gold").ToArray();

            return bags.Select(it => it.Name).Distinct().Count();
        }

        public override object GetAnswerTwo()
        {
            BagRule[] rules = GetRules().ToArray();
            BagRule br = rules.Single(it => it.Name == "shiny gold");

            return br.CountInside(rules);
        }

        private IEnumerable<BagRule> FindContainsRecursive(BagRule[] rules, string name)
        {
            var directlyContains = rules.Where(it => it.ContainedBags.Any(x => x.Key == name));

            foreach (var bDirectly in directlyContains)
            {
                yield return bDirectly;

                foreach (var bIndirectly in FindContainsRecursive(rules, bDirectly.Name))
                {
                    yield return bIndirectly;
                }
            }
        }

        private IEnumerable<BagRule> GetRules()
        {
            ConcurrentDictionary<string, BagRule> bagRules = new ConcurrentDictionary<string, BagRule>();

            foreach (var line in Lines)
            {
                int indexOfContain = line.IndexOf("contain");

                string nameSelf = line.Substring(0, indexOfContain - 1).Trim()
                    .Replace(" bags","")
                    .Replace(" bag", "");

                var bagSelf = bagRules.GetOrAdd(nameSelf, (x) => new BagRule(x));

                string containValueString = line.Substring(indexOfContain + 7).Trim();

                string[] containValues = containValueString.Split(new[] {','}).Select(it => it.Trim(new [] {' ', '.'})).ToArray();

                foreach (string containValue in containValues)
                {
                    if (containValue != "no other bags")
                    {
                        string countAsString = containValue.Split(new[] { ' ' }).First();
                        int containCount = Convert.ToInt32(countAsString);

                        string containName = containValue.Substring(countAsString.Length).Trim()
                            .Replace(" bags", "")
                            .Replace(" bag", "");

                        bagRules.GetOrAdd(containName, (x) => new BagRule(x));

                        bagSelf.ContainedBags.Add(containName, containCount);
                    }
                }
            }

            return bagRules.Values;
        }
    }

    public class BagRule
    {
        public BagRule(string name)
        {
            Name = name;
            ContainedBags = new Dictionary<string, int>();
        }

        public string Name { get;  }
        public Dictionary<string, int> ContainedBags { get; }

        public int CountInside(BagRule[] rules)
        {
            return ContainedBags.Sum(it => it.Value + it.Value * rules.Single(x => x.Name == it.Key).CountInside(rules));

        }
    }
}