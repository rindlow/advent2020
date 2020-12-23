using Xunit;
using Advent2020.CrabCups;
namespace Advent2020.Tests
{
    public class CrabCupsTest
    {
        [Theory]
        [InlineData("389125467", 10, "92658374")]
        [InlineData("389125467", 100, "67384529")]
        public void TestMoves(string labels, int moves, string expected)
        {
            Cups cups = new Cups(labels, 9);
            Assert.Equal(expected, cups.LabelAfterMoves(moves));
        }
        [Fact]
        public void TestProductOfFirstCups()
        {
            Cups cups = new Cups("389125467", 1000000);
            Assert.Equal(149245887792, cups.ProductOfFirstCups());
        }
    }
}