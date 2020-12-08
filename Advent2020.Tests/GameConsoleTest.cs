using Xunit;
namespace Advent2020
{
    public class GameConsoleTest
    {
        [Fact]
        public void TestRunUntilLoop()
        {
            GameConsole console = new GameConsole();
            console.ReadProgamFromFile("input/day8.txt");
            Assert.Equal(5, console.RunUntilLoop());
        }
        [Fact]
        public void TestRunUntilPatched()
        {
            GameConsole console = new GameConsole();
            console.ReadProgamFromFile("input/day8.txt");
            Assert.Equal(8, console.RunUntilPatched());
        }
    }
}