using System;
using System.Collections.Generic;
using System.IO;

namespace Advent2020
{
    public  class FileReader
    {
        public static List<T> ReadFile<T>(string filename, Func<string, T> transform)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                List<T> lines = new List<T>();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(transform(line));
                }
                return lines;
            }
        }
        public static List<int> ReadFileOfInts(string filename)
        {
            return ReadFile<int>(filename, (line) => int.Parse(line));
        }
        public static List<long> ReadFileOfLongs(string filename)
        {
            return ReadFile<long>(filename, (line) => long.Parse(line));
        }
        public static List<string> ReadFileOfStrings(string filename)
        {
            return ReadFile<string>(filename, (line) => line);
        }
    }
}
