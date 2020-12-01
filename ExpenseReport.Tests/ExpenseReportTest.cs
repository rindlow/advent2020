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
            Assert.Equal(new List<int> {1721, 299}, _expense.FindTwoEntries(
                new List<int> {1721, 979, 366, 299, 675, 1456}));
        }
        [Fact]
        public void TestFindThreeEntries()
        {
            Assert.Equal(new List<int> {979, 366, 675}, _expense.FindThreeEntries(
                new List<int> {1721, 979, 366, 299, 675, 1456}));
        }
        [Fact]
        public void TestReadFile()
        {
            Assert.Equal(6, _expense.ReadFile("test_input.txt").Count);
        }
        [Fact]
        public void TestMultiplyTwoEntriesFromFile()
        {
            Assert.Equal(514579, _expense.MultiplyTwoEntriesFromFile("test_input.txt"));
        }
        [Fact]
        public void TestMultiplyThreeEntriesFromFile()
        {
            Assert.Equal(241861950, _expense.MultiplyThreeEntriesFromFile("test_input.txt"));
        }
    }
}
