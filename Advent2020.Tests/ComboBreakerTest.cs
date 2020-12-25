using Xunit;
using Advent2020.ComboBreaker;
namespace Advent2020.Tests
{
    public class ComboBreakerTest
    {
        [Theory]
        [InlineData(7, 5764801, 8)]
        [InlineData(7, 17807724, 11)]
        public void TestFindExponent(int message, int encrypted, int expected)
        {
            Rsa rsa = new Rsa(20201227);
            Assert.Equal(expected, rsa.FindExponent(message, encrypted));
        }

        [Theory]
        [InlineData(5764801, 11, 14897079)]
        [InlineData(17807724, 8, 14897079)]
        public void TestEncrypt(int message, int exponent, int expected)
        {
            Rsa rsa = new Rsa(20201227);
            Assert.Equal(expected, rsa.Encrypt(message, exponent));
        }
        [Fact]
        public void TestFindEncryptionKey()
        {
            Rsa rsa = new Rsa(20201227);
            Assert.Equal(14897079, rsa.FindEncryptionKey(5764801, 17807724));
        }
        [Fact]
        public void TestFindEncryptionKeyFromFile()
        {
            Rsa rsa = new Rsa(20201227);
            Assert.Equal(14897079, rsa.FindEncryptionKeyFromFile("input/day25.txt"));
        }
    }
}