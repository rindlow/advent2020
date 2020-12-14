using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Advent2020
{
    public class PortComputer
    {
        Dictionary<long, long> Mem;
        public PortComputer()
        {
            Mem = new Dictionary<long, long>();
        }
        public static long ApplyMask(long value, string mask)
        {
            StringBuilder orMaskSB = new StringBuilder("");
            StringBuilder andMaskSB = new StringBuilder("");
            foreach (char c in mask)
            {
                if (c == '0' || c == '1')
                {
                    orMaskSB.Append(c);
                    andMaskSB.Append(c);
                }
                else 
                {
                    orMaskSB.Append('0');
                    andMaskSB.Append('1');
                }
            }
            long orMask = Convert.ToInt64(orMaskSB.ToString(), 2);
            long andMask = Convert.ToInt64(andMaskSB.ToString(), 2);
            long result = (value & andMask) | orMask;
            return result;
        }
        private List<long> FloatingAddresses(long address, string mask)
        {
            List<StringBuilder> stringBuilders = new List<StringBuilder>();
            string addr = Convert.ToString(address, 2).PadLeft(36, '0');
            stringBuilders.Add(new StringBuilder(""));
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0')
                {
                    foreach (StringBuilder sb in stringBuilders)
                    {
                        sb.Append(addr[i]);
                    }
                }
                else if (mask[i] == '1')
                {
                    foreach (StringBuilder sb in stringBuilders)
                    {
                        sb.Append('1');
                    }
                }
                else
                {
                    List<StringBuilder> newList = new List<StringBuilder>();
                    StringBuilder newSB;
                    foreach (StringBuilder sb in stringBuilders)
                    {
                        newSB = new StringBuilder(sb.ToString());
                        sb.Append('0');
                        newSB.Append('1');
                        newList.Add(newSB);
                    }
                    stringBuilders.AddRange(newList);
                }
            }
            return stringBuilders.Select(sb => Convert.ToInt64(sb.ToString(), 2)).ToList();
        }
        public long SumOfMemory()
        {
            return Mem.Values.Sum();
        }
        public void RunProgramFromFile(string filename, int version)
        {
            Regex maskRe = new Regex(@"mask = ([01X]{36})");
            Regex memRe = new Regex(@"mem\[(\d+)\] = (\d+)");
            string mask = "";
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                Match maskMatch = maskRe.Match(line);
                if (maskMatch.Success)
                {
                    mask = maskMatch.Groups[1].Value;
                    continue;
                }
                Match memMatch = memRe.Match(line);
                if (memMatch.Success)
                {
                    int addr = int.Parse(memMatch.Groups[1].Value);
                    int value = int.Parse(memMatch.Groups[2].Value);
                    if (version == 1)
                    {
                        Mem[addr] = ApplyMask(value, mask);
                    }
                    else if (version == 2)
                    {
                        foreach (long floating in FloatingAddresses(addr, mask))
                        {
                            Mem[floating] = value;
                        }
                    }
                }
            }
        }
        public void RunProgramWithBitmaskFromFile(string filename)
        {
            RunProgramFromFile(filename, 1);
        }
        public void RunProgramWithDecoderFromFile(string filename)
        {
            RunProgramFromFile(filename, 2);
        }
    }
}