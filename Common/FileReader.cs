using System;
using System.Collections.Generic;
using System.IO;

namespace Advent2020
{
    public  class FileReader
    {
        public static List<int> ReadFileOfInts(string filename)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                List<int> lines = new List<int>();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(int.Parse(line));
                }
                return lines;
            }
        }
    }
}
