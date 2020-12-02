using System;
using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class CommonTest
    {
        [Fact]
        public void TestReadFileOfInts()
        {
            Assert.Equal(6, FileReader.ReadFileOfInts("test_input.txt").Count);
        }
        [Fact]
        public void TestReadFileOfStrings()
        {
            Assert.Equal(6, FileReader.ReadFileOfStrings("test_input.txt").Count);
        }
        [Fact]
        public void TestCombinations()
        {
            List<List<string>> expected = new List<List<string>>();
            expected.Add(new List<string> {"A", "B"});
            expected.Add(new List<string> {"A", "C"});
            expected.Add(new List<string> {"A", "D"});
            expected.Add(new List<string> {"B", "C"});
            expected.Add(new List<string> {"B", "D"});
            expected.Add(new List<string> {"C", "D"});
            Assert.Equal(expected, IterTools<string>.Combinations(new List<string> { "A", "B", "C", "D"}, 2));
        }
    }
}
