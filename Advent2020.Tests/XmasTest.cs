using Xunit;
namespace Advent2020
{
    public class XmasTest
    {
        [Fact]
        public void TestFirstInvalidNumber()
        {
            Xmas xmas = new Xmas("input/day9.txt", 5);
            Assert.Equal(127, xmas.FirstInvalidNumber());
        }
        [Fact]
        public void TestFindWeakness()
        {
            Xmas xmas = new Xmas("input/day9.txt", 5);
            Assert.Equal(62, xmas.FindWeakness());
        }
    }
}