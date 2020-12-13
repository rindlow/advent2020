using Xunit;
namespace Advent2020
{
    public class BusTest
    {
        [Fact]
        public void TestNextBus()
        {
             Bus bus = new Bus();
             bus.ParseSchedule("7,13,x,x,59,x,31,19");
             Assert.Equal(59, bus.NextBus(939));
        }
        [Fact]
        public void TestMinutesToBus()
        {
            Assert.Equal(5, Bus.MinutesToBus(59, 939));
        }
        [Fact]
        public void TestNextBusTimesWaitFromFile()
        {
            Bus bus = new Bus();
            Assert.Equal(295, bus.NextBusTimesWaitFromFile("input/day13.txt"));
        }
    }
}