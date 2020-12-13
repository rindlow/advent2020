using Xunit;
namespace Advent2020
{
    public class SeatingTest
    {
        [Fact]
        public void TestRunUntilNoChange()
        {
            Seating seating = new Seating("input/day11.txt");
            seating.RunUntilNoChange();
            Assert.Equal(37, seating.OccupiedSeats());
        }
        [Fact]
        public void TestRunLineOfSightUntilNoChange()
        {
            Seating seating = new Seating("input/day11.txt");
            seating.RunLineOfSightUntilNoChange();
            Assert.Equal(26, seating.OccupiedSeats());
        }
    }
}