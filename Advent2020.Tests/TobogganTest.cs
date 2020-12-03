using Xunit;

namespace Advent2020
{
    public class TobogganTest
    {
        [Fact]
        public void TestCollisions()
        {
            Toboggan toboggan = new Toboggan("input/day3.txt");
            Assert.Equal(7, toboggan.Collisions(3, 1));
        }
        [Fact]
        public void TestCollisionProduct()
        {
            Toboggan toboggan = new Toboggan("input/day3.txt");
            Assert.Equal(336, toboggan.CollisionProduct(new int[,] {{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}}));
        }
    }
}