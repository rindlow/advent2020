using Xunit;
using Advent2020.ImageTiles;
namespace Advent2020.Tests
{
    public class ImageTilesTest
    {
        [Fact]
        public void TestProductOfCornerTiles()
        {
            Image image = new Image("input/day20.txt");
            Assert.Equal(20899048083289, image.ProductOfCornerTiles());
        }
        [Fact]
        public void TestRoughness()
        {
            Image image = new Image("input/day20.txt");
            Assert.Equal(273, image.Roughness());
        }
    }

}