using Xunit;

namespace Advent2020
{
    public class LuggageProcessTest
    {
        [Fact]
        public void TestNumberOfContainingBags()
        {
            LuggageProcess process = new LuggageProcess("input/day7.txt");
            Assert.Equal(4, process.NumberOfContainingBags("shiny gold"));
        }
        [Theory]
        [InlineData("input/day7.txt", 32)]
        [InlineData("input/day7b.txt", 126)]
        public void TestNumberOfBagsInBag(string filename, int expected)
        {
            LuggageProcess process = new LuggageProcess(filename);
            Assert.Equal(expected, process.NumberOfBagsInBag("shiny gold"));
        }
    }
}