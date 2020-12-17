using Xunit;
namespace Advent2020
{
    public class ConwayCubeTest
    {
        [Fact]
        public void TestNumberOfActiveCubes()
        {
            ConwayCube cube = new ConwayCube("input/day17.txt");
            cube.Run(6, 3);
            Assert.Equal(112, cube.NumberOfActiveCubes());
        }
        [Fact]
        public void TestNumberOfActiveCubes4D()
        {
            ConwayCube cube = new ConwayCube("input/day17.txt");
            cube.Run(6, 4);
            Assert.Equal(848, cube.NumberOfActiveCubes());
        }
    }
}