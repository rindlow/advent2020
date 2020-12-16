using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Advent2020
{
    class Field
    {
        int FirstLower;
        int FirstUpper;
        int SecondLower;
        int SecondUpper;
        public Field(int firstlower, int firstupper, int secondlower, int secondupper)
        {
            FirstLower = firstlower;
            FirstUpper = firstupper;
            SecondLower = secondlower;
            SecondUpper = secondupper;
        }
        public bool IsValid(int value)
        {
            return ((value >= FirstLower && value <= FirstUpper)
                    || (value >= SecondLower && value <= SecondUpper));
        }
    }
    public class Ticket
    {
        Dictionary<string, Field> Fields;
        List<int> Errors;
        List<int> MyTicket;
        List<List<int>> NearbyTickets;
        Dictionary<string, int> FieldIndex;
        public Ticket(string filename)
        {
            Fields = new Dictionary<string, Field>();
            Errors = new List<int>();
            MyTicket = new List<int>();
            NearbyTickets = new List<List<int>>();
            FieldIndex = new Dictionary<string, int>();

            ReadFile(filename);
        }
        private void ReadFile(string filename)
        {
            bool checkTickets = false;
            bool parseMyTicket = false;
            Regex fieldRe = new Regex(@"(.*): (\d+)-(\d+) or (\d+)-(\d+)");

            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                Match match = fieldRe.Match(line);
                if (match.Success)
                {
                    Fields[match.Groups[1].Value] = new Field(int.Parse(match.Groups[2].Value),
                                                              int.Parse(match.Groups[3].Value),
                                                              int.Parse(match.Groups[4].Value),
                                                              int.Parse(match.Groups[5].Value));
                }
                if (line == "your ticket:")
                {
                    parseMyTicket = true;
                    continue;
                }
                if (parseMyTicket)
                {
                    foreach (string strValue in line.Split(','))
                    {
                        MyTicket.Add(int.Parse(strValue));
                    }
                    parseMyTicket = false;
                    continue;
                }
                if (line == "nearby tickets:")
                {
                    checkTickets = true;
                    continue;
                }
                if (checkTickets)
                {
                    bool ticketValid = true;
                    List<int> ticket = new List<int>();
                    foreach (string strValue in line.Split(','))
                    {
                        int value = int.Parse(strValue);
                        bool valid = false;
                        foreach (Field field in Fields.Values)
                        {
                            if (field.IsValid(value))
                            {
                                valid = true;
                                break;
                            }
                        }
                        if (valid)
                        {
                            ticket.Add(value);
                        }
                        else
                        {
                            Errors.Add(value);
                            ticketValid = false;
                        }
                    }
                    if (ticketValid)
                    {
                        NearbyTickets.Add(ticket);
                    }
                }
            }
        }
        public long ScanningErrorRate()
        {
            return Errors.Sum();
        }
        public void IdentifyFields()
        {
            Dictionary<Tuple<string, int>, bool> possible = new Dictionary<Tuple<string, int>, bool>();
            for (int i = 0; i < MyTicket.Count; i++)
            {
                foreach (KeyValuePair<string, Field> kvp in Fields)
                {
                    possible[new Tuple<string, int>(kvp.Key, i)] = true;
                }
            }
            foreach (List<int> ticket in NearbyTickets)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    foreach (KeyValuePair<string, Field> kvp in Fields)
                    {
                        if (!kvp.Value.IsValid(ticket[i]))
                        {
                            possible[new Tuple<string, int>(kvp.Key, i)] = false;
                        }
                    }
                }
            }
            while (FieldIndex.Count < Fields.Count)
            {
                foreach (KeyValuePair<string, Field> kvp in Fields)
                {
                    if (FieldIndex.ContainsKey(kvp.Key))
                    {
                        continue;
                    }
                    int nPossible = 0;
                    int lastPossible = -1;
                    for (int i = 0; i < MyTicket.Count; i++)
                    {
                        if (possible[new Tuple<string, int>(kvp.Key, i)])
                        {
                            nPossible++;
                            lastPossible = i;
                        }
                    }
                    if (nPossible == 0)
                    {
                        throw new Exception($"No valid field for {kvp.Key} found");
                    }
                    if (nPossible == 1)
                    {
                        FieldIndex[kvp.Key] = lastPossible;
                        foreach (KeyValuePair<string, Field> kvp2 in Fields)
                        {
                            possible[new Tuple<string, int>(kvp2.Key, lastPossible)] = false;
                        }
                    }
                }
            }
        }
        public int Field(string name)
        {
            return MyTicket[FieldIndex[name]];
        }
        public long ProductOfFieldsContaining(string containing)
        {
            return FieldIndex.Where(i => i.Key.Contains(containing)).Aggregate(1L, (p, i) => p * MyTicket[i.Value], p => p);
        }
    }
}