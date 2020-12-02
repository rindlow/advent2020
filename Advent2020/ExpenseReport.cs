using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class ExpenseReport
    {
        public List<int> FindEntries(List<int> list, int numEntries, int target)
        {
            foreach (List<int> entries in IterTools<int>.Combinations(list, numEntries))
            {
                if (entries.Sum() == target)
                {
                    return entries;
                }
            }
            throw new Exception("No matching entries found");
        }
        public int MultiplyEntriesFromFile(string filename, int numEntries, int target)
        {
            List<int> entries = FindEntries(FileReader.ReadFileOfInts(filename), numEntries, target);
            return entries.Aggregate(1, (product, i) => product * i, product => product);
        }
    }
}
