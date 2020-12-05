using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class BoardingPassTest
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void TestParseBoardingPass(string input, int expected)
        {
            Assert.Equal(expected, BoardingPass.ParseBoardingPass(input));
        }
        [Fact]
        public void TestHighestSeatId()
        {
            Assert.Equal(820, BoardingPass.HighestSeatIdInFile("input/day5.txt"));
        }
        [Fact]
        public void TestFindGapInList()
        {
            List<int> list = new List<int> {5, 6, 7, 9, 10, 11};
            Assert.Equal(8, BoardingPass.FindGapInList(list));
        }
    }
}