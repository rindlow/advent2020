using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent2020
{
    public class ExpenseReport
    {
        public List<int> ReadFile(string filename)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                List<int> lines = new List<int>();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(int.Parse(line));
                }
                return lines;
            }
        }
        public List<int> FindTwoEntries(List<int> list)
        {
            foreach (int x in list.GetRange(0, list.Count - 1))
            {
                foreach (int y in list.GetRange(1, list.Count - 1))
                {
                    if (x + y == 2020)
                    {
                        return new List<int> {x, y};
                    }
                }
            }
            throw new Exception("No matching entries found");
        }
        public List<int> FindThreeEntries(List<int> list)
        {
            foreach (int x in list.GetRange(0, list.Count - 2))
            {
                foreach (int y in list.GetRange(1, list.Count - 2))
                {
                    foreach (int z in list.GetRange(2, list.Count - 2))
                    {
                        if (x + y + z == 2020)
                        {
                            return new List<int> {x, y, z};
                        }
                    }
                }
            }
            throw new Exception("No matching entries found");
        }
        public int MultiplyTwoEntriesFromFile(string filename)
        {
            List<int> entries = FindTwoEntries(ReadFile(filename));
            return entries[0] * entries[1];
        }
        public int MultiplyThreeEntriesFromFile(string filename)
        {
            List<int> entries = FindThreeEntries(ReadFile(filename));
            return entries[0] * entries[1] * entries[2];
        }
    }
}
