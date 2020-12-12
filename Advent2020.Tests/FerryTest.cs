using Xunit;
namespace Advent2020
{
    public class FerryTest
    {
        [Fact]
        public void TestNavigateFromFile()
        {
            Ferry ferry = new Ferry();
            ferry.NavigateFromFile("input/day12.txt");
            Assert.Equal(25, ferry.DistanceFromStart());
        }
        [Fact]
        public void TestNavigateByWaypointFromFile()
        {
            Ferry ferry = new Ferry();
            ferry.NavigateByWaypointFromFile("input/day12.txt");
            Assert.Equal(286, ferry.DistanceFromStart());
        }
    }
}