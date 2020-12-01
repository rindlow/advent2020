using System;
using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class ExpenseReportTests
    {
        private ExpenseReport _expense;

        public ExpenseReportTests()
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
            Assert.Equal(514579, _expense.MultiplyEntriesFromFile("test_input.txt", 2, 2020));
        }
        [Fact]
        public void TestMultiplyThreeEntriesFromFile()
        {
            Assert.Equal(241861950, _expense.MultiplyEntriesFromFile("test_input.txt", 3, 2020));
        }
    }
}
