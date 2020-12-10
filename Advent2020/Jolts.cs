using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020
{
    public class Jolts
    {
        private List<int> Adapters;
        private Dictionary<int, long> ArrangementsCache;
        public Jolts(string filename)
        {
            Adapters = AdaptersFromFile(filename);
            ArrangementsCache = new Dictionary<int, long>();
            ArrangementsCache[Adapters.Count - 1] = 1;
        }
        private List<int> AdaptersFromFile(string filename)
        {
            List<int> adapters = FileReader.ReadFileOfInts(filename);
            adapters.Add(0);
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();
            return adapters;
        }
        public int Differences()
        {

            IEnumerable<int> differences = Adapters.GetRange(0, Adapters.Count - 1).Zip(Adapters.GetRange(1, Adapters.Count - 1), (a, b) => b - a);
            int ones = differences.Count(diff => diff == 1);
            int threes = differences.Count(diff => diff == 3);
            return ones * threes;
        }
        private long ComputeArrangements(int index)
        {
            if (ArrangementsCache.ContainsKey(index))
            {
                return ArrangementsCache[index];
            }
            long arr = 0;
            for (int i = index + 1; i < Math.Min(index + 4, Adapters.Count); i++)
            {
                if (Adapters[i] - Adapters[index] < 4)
                {
                    arr += ComputeArrangements(i);
                }
            }
            ArrangementsCache[index] = arr;
            return arr;
        }
        public long Arrangements()
        {
            return ComputeArrangements(0);
        }

    }
}