using System;
using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class ExpenseReportTest
    {
        private ExpenseReport _expense;

        public ExpenseReportTest()
        {
            _expense = new ExpenseReport();
        }
        [Fact]
        public void TestFindTwoEntries()
        {
            Assert.Equal(new List<int> {1721, 299}, _expense.FindEntries(
                new List<int> {1721, 979, 366, 299, 675, 1456}, 2, 2020));
        }
        [Fact]
        public void TestFindThreeEntries()
        {
            Assert.Equal(new List<int> {979, 366, 675}, _expense.FindEntries(
                new List<int> {1721, 979, 366, 299, 675, 1456}, 3, 2020));
        }
        [Fact]
        public void TestMultiplyTwoEntriesFromFile()
        {
            Assert.Equal(514579, _expense.MultiplyEntriesFromFile("input/day1.txt", 2, 2020));
        }
        [Fact]
        public void TestMultiplyThreeEntriesFromFile()
        {
            Assert.Equal(241861950, _expense.MultiplyEntriesFromFile("input/day1.txt", 3, 2020));
        }
    }
}
