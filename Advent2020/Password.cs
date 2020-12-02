using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Password
    {
        public static bool ParseAndApplyChecker(string input, Func<int, int, char, string, bool> checker)
        {
            string pattern = @"^(\d+)-(\d+) (\w): (\w*)$";
            Match match = Regex.Match(input, pattern);
            if (!match.Success)
            {
                throw new Exception($"Line '{input}' does not match");
            }
            int first = int.Parse(match.Groups[1].Value);
            int second = int.Parse(match.Groups[2].Value);
            char character = match.Groups[3].Value[0];
            string password = match.Groups[4].Value;

            return checker(first, second, character, password);
        }

        public static bool ParseAndCheck(string input) {
            return ParseAndApplyChecker(input, (first, second, character, password) => {
                int occurences = password.Split(character).Length - 1;
                return occurences >= first && occurences <= second;
            });
        }
        public static bool ParseAndCheckToboggan(string input)
        {
            return ParseAndApplyChecker(input, (first, second, character, password) => {
                return password[first - 1] == character ^ password[second - 1] == character;
            });
        }
        public static int CheckPasswordsFromFile(string filename)
        {
            return FileReader.ReadFileOfStrings(filename).Count(line => ParseAndCheck(line));
        }
        public static int CheckPasswordsFromFileToboggan(string filename)
        {
            return FileReader.ReadFileOfStrings(filename).Count(line => ParseAndCheckToboggan(line));
        }
    }
}
