using Xunit;
namespace Advent2020
{
    public class JoltsTest
    {
        [Fact]
        public void TestDifferences()
        {
            Jolts jolts = new Jolts("input/day10.txt");
            Assert.Equal(35, jolts.Differences());
        }
        [Fact]
        public void TestArrangements()
        {
            Jolts jolts = new Jolts("input/day10.txt");
            Assert.Equal(8, jolts.Arrangements());
        }
    }
}