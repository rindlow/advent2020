using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class IterTools<T>
    {
        public static List<List<T>> Combinations(List<T> list, int k)
        {
            if (k == 1)
            {
                return list.Select(item => new List<T> { item }).ToList();
            }
            List<List<T>> result = new List<List<T>>();
            for (int i = 0; i < list.Count; i++)
            {
                List<List<T>> cmb = Combinations(list.GetRange(i + 1, list.Count - i - 1), k - 1);
                foreach (List<T> item in cmb)
                {
                    item.Insert(0, list[i]);
                }
                result.AddRange(cmb);
            }
            return result;
        }
    }
}