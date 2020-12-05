using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class BoardingPass
    {
        public static int ParseBoardingPass(string input)
        {
            string binary = input.Replace('B', '1').Replace('F', '0').Replace('R', '1').Replace('L', '0');
            return Convert.ToInt32(binary, 2);
        }
        public static List<int> ReadBoardingPassesFromFile(string filename)
        {
            return FileReader.ReadFileOfStrings(filename).Select(pass => ParseBoardingPass(pass)).ToList();
        }
        public static int HighestSeatIdInFile(string filename)
        {
            return ReadBoardingPassesFromFile(filename).Max();
        }
        public static int FindGapInList(List<int> list)
        {
            list.Sort();
            for (int i = 1; i < list.Count - 1; i++)
            {
                if (list[i] - list[i - 1] == 2)
                {
                    return list[i] - 1;
                }
            }
            return 0;
        }
        public static int FindGapInFile(string filename)
        {
            return FindGapInList(ReadBoardingPassesFromFile(filename));
        }
    }
}