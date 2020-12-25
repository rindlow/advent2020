using Xunit;
using Advent2020.Lobby;
namespace Advent2020.Tests
{
    public class LobbyTest
    {
        [Fact]
        public void TestNumberOfBlackTiles()
        {
            Floor floor = new Floor();
            floor.FlipTilesFromFile("input/day24.txt");
            Assert.Equal(10, floor.NumberOfBlackTiles());
        }
        [Theory]
        [InlineData(1, 15)]
        [InlineData(2, 12)]
        [InlineData(100, 2208)]
        public void TestNumberOfBlackTilesAfterDays(int days, int expected)
        {
            Floor floor = new Floor();
            floor.FlipTilesFromFile("input/day24.txt");
            floor.ApplyRulesForDays(days);
            Assert.Equal(expected, floor.NumberOfBlackTiles());
        }
    }
}