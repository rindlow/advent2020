using Xunit;

namespace Advent2020
{
    public class PassportTest
    {
        [Fact]
        public void TestCheckEmptyPassport()
        {
            PassportInfo passport = new PassportInfo();
            Assert.False(passport.ValuesPresent());
        }
        [Fact]
        public void TestCheckPassport()
        {
            PassportInfo passport = new PassportInfo();
            passport.ecl = "gry";
            passport.pid = "860033327";
            passport.eyr = 2020;
            passport.hcl = "#fffffd";
            passport.byr = 1937;
            passport.iyr = 2017;
            passport.cid = 147;
            passport.hgt = "183cm";
            Assert.True(passport.ValuesPresent());
        }
        [Fact]
        public void TestPassportsFromFile()
        {
            Assert.Equal(2, Passport.CheckPassportsFromFile("input/day4.txt"));
        }
        [Fact]
        public void TestInvalidPasswords()
        {
            Assert.Equal(0, Passport.ValidatePassportsFromFile("input/day4_invalid.txt"));
        }
        [Fact]
        public void TestValidPasswords()
        {
            Assert.Equal(4, Passport.ValidatePassportsFromFile("input/day4_valid.txt"));
        }
    }
}