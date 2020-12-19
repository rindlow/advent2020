using Xunit;
namespace Advent2020
{
    public class MonsterMessageTest
    {
        [Fact]
        public void TestNumberOfMatchesFromFile()
        {
            MonsterMessage message = new MonsterMessage();
            Assert.Equal(2, message.NumberOfMatchesFromFile("input/day19.txt", false));
        }
        [Fact]
        public void TestNumberOfMatchesFromPatchedFile()
        {
            MonsterMessage message = new MonsterMessage();
            Assert.Equal(12, message.NumberOfMatchesFromFile("input/day19b.txt", true));
        }
    }
}