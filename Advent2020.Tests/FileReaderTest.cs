using System;
using System.Collections.Generic;
using Xunit;

namespace Advent2020
{
    public class FileReaderTest
    {
        [Fact]
        public void TestReadFileOfInts()
        {
            Assert.Equal(6, FileReader.ReadFileOfInts("input/day1.txt").Count);
        }
        [Fact]
        public void TestReadFileOfStrings()
        {
            Assert.Equal(6, FileReader.ReadFileOfStrings("input/day1.txt").Count);
        }
    }
}
