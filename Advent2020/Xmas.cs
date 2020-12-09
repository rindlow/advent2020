using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Xmas
    {
        private List<long> Numbers;
        private int WindowLen;
        public Xmas(string filename, int windowLen)
        {
            Numbers = FileReader.ReadFileOfLongs(filename);
            WindowLen = windowLen;
        }
        private bool NumberInWindow(int index)
        {
            long number = Numbers[index];
            for (int i = index - WindowLen; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (Numbers[i] + Numbers[j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public long FirstInvalidNumber()
        {
            for (int i = WindowLen; i < Numbers.Count; i++)
            {
                if (!NumberInWindow(i))
                {
                    return Numbers[i];
                }
            }
            return -1;
        }
        public long FindWeakness()
        {
            long number = FirstInvalidNumber();
            for (int i = 0; i < Numbers.Count; i++)
            {
                for (int j = 1; j < Numbers.Count - i; j++)
                {
                    List<long> set = Numbers.GetRange(i, j);
                    long sum = set.Sum();
                    if (sum > number)
                    {
                        break;
                    }
                    if (sum == number)
                    {
                        return set.Max() + set.Min();
                    }
                }
            }
            return -1;
        }
    }
}