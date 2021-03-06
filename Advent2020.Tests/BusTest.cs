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
        [Fact]
        public void TestExtendedGCD()
        {
            long x = 0;
            long y = 0;
            Assert.Equal(2, Bus.ExtendedGCD(240, 46, out x, out y));
            Assert.Equal(-9, x);
            Assert.Equal(47, y);
        }
        [Theory]
        [InlineData("17,x,13,19", 3417)]
        [InlineData("67,7,59,61", 754018)]
        [InlineData("67,x,7,59,61", 779210)]
        [InlineData("67,7,x,59,61", 1261476)]
        [InlineData("1789,37,47,1889", 1202161486)]
        public void TestStartTimestamp(string schedule, long expected)
        {
            Bus bus = new Bus();
            bus.ParseSchedule(schedule);
            Assert.Equal(expected, bus.StartTimestamp());
        }
        [Fact]
        public void TestStartTimeStampFromFile()
        {
            Bus bus = new Bus();
            Assert.Equal(1068781, bus.StartTimestampFromFile("input/day13.txt"));
        }
    }
}
