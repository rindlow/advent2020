using Xunit;
namespace Advent2020
{
    public class PortComputerTest
    {
        [Theory]
        [InlineData(11, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 73)]
        [InlineData(101, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101)]
        [InlineData(0, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 64)]
        public void TestApplyMask(long value, string mask, long expected)
        {
            Assert.Equal(expected, PortComputer.ApplyMask(value, mask));
        }
        [Fact]
        public void TestRunProgramWithBitmaskFromFile()
        {
            PortComputer pc = new PortComputer();
            pc.RunProgramWithBitmaskFromFile("input/day14.txt");
            Assert.Equal(165, pc.SumOfMemory());
        }
        [Fact]
        public void TestRunProgramWithDecoderFromFile()
        {
            PortComputer pc = new PortComputer();
            pc.RunProgramWithDecoderFromFile("input/day14b.txt");
            Assert.Equal(208, pc.SumOfMemory());
        }
    }
}