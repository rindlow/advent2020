using Xunit;
using Advent2020.CrabCombat;
namespace Advent2020.Tests
{
    public class CrabCombatTest
    {
        [Fact]
        public void TestScore()
        {
            Combat combat = new Combat("input/day22.txt");
            combat.Play();
            Assert.Equal(306, combat.WinnerScore());
        }
        [Fact]
        public void TestRecursiveScore()
        {
            Combat combat = new Combat("input/day22.txt");
            combat.PlayRecursive();
            Assert.Equal(291, combat.WinnerScore());
        }
    }
}