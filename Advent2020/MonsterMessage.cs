using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Advent2020
{
    public class MonsterMessage
    {
        Regex Re;
        List<string> Input;
        Dictionary<string, string> Rules;
        Dictionary<string, int> Used;
        public MonsterMessage()
        {
            Input = new List<string>();
            Rules = new Dictionary<string, string>();
            Used = new Dictionary<string, int>();
            Used["8"] = 0;
            Used["11"] = 0;
        }
        public int NumberOfMatchesFromFile(string filename, bool patch)
        {
            ReadFile(filename);
            if (patch)
            {
                PatchRules();
            }
            string rtr = RulesToRegex("0");
            Re = new Regex($"^{rtr}$");
            return Input.Where(s => MatchString(s)).Count();
        }
        void ReadFile(string filename)
        {
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                if (line.Contains(':'))
                {
                    string[] kv = line.Split(": ");
                    Rules[kv[0]] = kv[1];
                }
                else if (line == "")
                {
                    continue;
                }
                else
                {
                    Input.Add(line);
                }
            }
        }
        string ConcatRules(string rules)
        {
            string s = String.Join("", rules.Split(" ").Select(r => RulesToRegex(r)));
            return $"({s})";
        }
        string RulesToRegex(string start)
        {
            string rule = Rules[start];
            if (Used.ContainsKey(start))
            {
                if (Used[start] > 20)
                {
                    return "X";
                }
                Used[start]++;
            }
            // else
            // {
            //     Used[start] = 1;
            // }
            if (rule.StartsWith('"'))
            {
                return rule.Substring(1, 1);
            }
            if (rule.Contains('|'))
            {
                string r = String.Join('|', rule.Split(" | ").Select(r => ConcatRules(r)));
                return $"({r})";
            }
            return ConcatRules(rule);

        }
        bool MatchString(string input)
        {
            return Re.Match(input).Success;
        }
        public void PatchRules()
        {
            Rules["8"] = "42 | 42 8";
            Rules["11"] = "42 31 | 42 11 31";
        }
    }
}