using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class CustomsDeclarationTest
    {
        [Fact]
        public void TestCustomsDeclarationGroup()
        {
            CustomsDeclarationGroup group = new CustomsDeclarationGroup();
            group.AddPerson("abcx");
            group.AddPerson("abcy");
            group.AddPerson("abcz");
            Assert.Equal(6, group.Count());
        }
        [Fact]
        public void TestCustomsDeclarationGroupEveryone()
        {
            CustomsDeclarationGroup group = new CustomsDeclarationGroup();
            group.AddPerson("abcx");
            group.AddPerson("abcy");
            group.AddPerson("abcz");
            Assert.Equal(3, group.CountWhereEveryone());
        }
        [Fact]
        public void TestSumDeclarationsFromFile()
        {
            Assert.Equal(11, CustomsDeclaration.SumDeclarationsFromFile("input/day6.txt"));
        }
        [Fact]
        public void TestSumDeclarationsEveryoneFromFile()
        {
            Assert.Equal(6, CustomsDeclaration.SumDeclarationsEveryoneFromFile("input/day6.txt"));
        }
    }
}