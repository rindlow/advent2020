using System;
using Xunit;

namespace Advent2020
{
    public class PasswordTest
    {
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void TestValidatePassword(string input, bool expected)
        {
            Assert.Equal(expected, Password.ParseAndCheck(input));
        }
        [Fact]
        public void TestPasswordsFromString()
        {
            Assert.Equal(2, Password.CheckPasswordsFromFile("test_input.txt"));
        }
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void TestValidatePasswordToboggan(string input, bool expected)
        {
            Assert.Equal(expected, Password.ParseAndCheckToboggan(input));
        }
                [Fact]
        public void TestPasswordsFromStringToboggan()
        {
            Assert.Equal(1, Password.CheckPasswordsFromFileToboggan("test_input.txt"));
        }
    }
}
