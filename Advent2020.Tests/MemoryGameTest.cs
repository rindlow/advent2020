using Xunit;
namespace Advent2020
{
    public class MemoryGameTest
    {
        [Theory]
        [InlineData("0,3,6", 436)]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        public void TestNumber2020(string startingNumbers, int expected)
        {
            Assert.Equal(expected, MemoryGame.Number(startingNumbers, 2020));
        }
        [Theory]
        [InlineData("0,3,6", 10, 0)]
        [InlineData("0,3,6", 2020, 436)]
        [InlineData("1,3,2", 2020, 1)]
        [InlineData("2,1,3", 2020, 10)]
        [InlineData("0,3,6", 30000000, 175594)]
        [InlineData("1,3,2", 30000000, 2578)]
        [InlineData("2,1,3", 30000000, 3544142)]
        public void TestNumber(string startingNumbers, int iterations, int expected)
        {
            Assert.Equal(expected, MemoryGame.Number(startingNumbers, iterations));
        }
    }
}