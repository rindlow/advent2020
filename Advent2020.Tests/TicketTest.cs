using Xunit;
namespace Advent2020
{
    public class TicketTest
    {
        [Fact]
        public void TestTicketScanningRate()
        {
            Ticket ticket = new Ticket("input/day16.txt");
            Assert.Equal(71, ticket.ScanningErrorRate());
        }
        [Fact]
        public void TestIdentifyFields()
        {
            Ticket ticket = new Ticket("input/day16b.txt");
            ticket.IdentifyFields();
            Assert.Equal(12, ticket.Field("class"));
            Assert.Equal(11, ticket.Field("row" ));
            Assert.Equal(13, ticket.Field("seat"));
        }
        [Fact]
        public void TestProductOfFields()
        {
            Ticket ticket = new Ticket("input/day16b.txt");
            ticket.IdentifyFields();
            Assert.Equal(156, ticket.ProductOfFieldsContaining("a"));
        }
    }
}