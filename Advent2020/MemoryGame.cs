using System;
using System.Collections.Generic;
namespace Advent2020
{
    public class MemoryGame
    {
        public static int Number(string startingNumbers, int iterations)
        {
            Dictionary<int, int> lastSeen = new Dictionary<int, int>();
            int index = 0;
            int prevIndex;
            int number = -1;
            foreach (string num in startingNumbers.Split(','))
            {
                number = int.Parse(num);
                lastSeen[number] = index++;
            }
            prevIndex = -1;
            while (index < iterations)
            {
                if (prevIndex < 0)
                {
                    number = 0;
                }
                else
                {
                    number = index - prevIndex - 1;
                }
                if (lastSeen.ContainsKey(number))
                {
                    prevIndex = lastSeen[number];
                }
                else
                {
                    prevIndex = -1;
                }
                lastSeen[number] = index++;
            }
            return number;
        }
    }
}