using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class CustomsDeclarationGroup
    {
        private Dictionary<char, int> answers;
        private int Size;

        public CustomsDeclarationGroup()
        {
            answers = new Dictionary<char, int>();
            Size = 0;
        }
        public void AddPerson(string person)
        {
            Size += 1;
            foreach (char answer in person)
            {
                if (answers.ContainsKey(answer))
                {
                    answers[answer] += 1;
                }
                else
                {
                    answers.Add(answer, 1);
                }
            }
        }
        public int Count()
        {
            return answers.Count;
        }
        public int CountWhereEveryone()
        {
            return answers.Count(kvp => kvp.Value == Size);
        }
    }
    public class CustomsDeclaration
    {
        public static List<CustomsDeclarationGroup> ParseDeclarations(List<string> declarations)
        {
            List<CustomsDeclarationGroup> groups = new List<CustomsDeclarationGroup>();
            CustomsDeclarationGroup group = new CustomsDeclarationGroup();
            foreach (string line in declarations)
            {
                if (line == "")
                {
                    groups.Add(group);
                    group = new CustomsDeclarationGroup();
                }
                else
                {
                    group.AddPerson(line);
                }
            }
            groups.Add(group);
            return groups;
        }
        public static int SumDeclarationsFromFile(string filename)
        {
            return ParseDeclarations(FileReader.ReadFileOfStrings(filename)).Select(group => group.Count()).Sum();
        }    
        public static int SumDeclarationsEveryoneFromFile(string filename)
        {
            return ParseDeclarations(FileReader.ReadFileOfStrings(filename)).Select(group => group.CountWhereEveryone()).Sum();
        }
    }
}