using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class BagRule
    {
        public string Outer;
	    public List<BagAmount> Content;
    }   
    public class BagAmount
    {
        public string Type;
	    public int Amount;
    }
    public class LuggageProcess
    {
        private Dictionary<string, List<BagAmount>> Rules;
            public LuggageProcess(string filename)
            {
                ReadRulesFromFile(filename);
            }
            public void ReadRulesFromFile(string filename)
            {
                Regex lineRe = new Regex(@"(.*) bags contain (.*)\.");
                Regex bagsRe = new Regex(@"(\d+) (.*?) bags?");
                Rules = new Dictionary<string, List<BagAmount>>();
                foreach (string line in FileReader.ReadFileOfStrings(filename))
                {
                    Match lineMatch = lineRe.Match(line);
                    if (!lineMatch.Success)
                    {
                        throw new Exception($"No bags found in line '{line}'");
                    }
                    List<BagAmount> bagList = new List<BagAmount>();
                    foreach (Match bagMatch in bagsRe.Matches(lineMatch.Groups[2].Value))
                    {
                        BagAmount bagAmount = new BagAmount();
                        bagAmount.Type = bagMatch.Groups[2].Value;
                        bagAmount.Amount = int.Parse(bagMatch.Groups[1].Value);
                        bagList.Add(bagAmount);
                    }
                    Rules[lineMatch.Groups[1].Value] = bagList;                }
            }
            public HashSet<string> ContainingBags(string bagtype, HashSet<string> seen)
            {
                HashSet<string> containing = new HashSet<string>();
                foreach (string rule in Rules.Keys)
                {
                    foreach (BagAmount bagAmount in Rules[rule])
                    {
                        if (bagAmount.Type == bagtype)
                        {
                            containing.Add(rule);
                        }
                    }
                }
                HashSet<string> newSet = new HashSet<string>();
                foreach (string bag in containing)
                {
                    if (!seen.Contains(bag))
                    {
                        newSet.UnionWith(ContainingBags(bag, containing));
                    }
                }
                containing.UnionWith(newSet);
                return containing;
            }
            public int NumberOfContainingBags(string bagtype)
            {
                return ContainingBags(bagtype, new HashSet<string>()).Count;
            }
            public int NumberOfBagsIncludingBag(string bagtype)
            {
                return 1 + Rules[bagtype].Select(ba => ba.Amount * NumberOfBagsIncludingBag(ba.Type)).Sum();
            }
            public int NumberOfBagsInBag(string bagtype)
            {
                return NumberOfBagsIncludingBag(bagtype) - 1;
            }
        }
}
