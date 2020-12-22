using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020.CrabCombat
{
    public class Combat
    {
        Dictionary<int, List<int>> Decks;
        int Winner;
        List<List<List<int>>> Seen;
        static int Game = 0;
        int GameNo;
        public Combat(string filename)
        {
            Decks = new Dictionary<int, List<int>>();
            Winner = 0;
            Seen = new List<List<List<int>>>();
            GameNo = ++Game;
            ReadFile(filename);
        }
        public Combat(Dictionary<int, List<int>> decks)
        {
            Decks = decks;
            Winner = 0;
            Seen = new List<List<List<int>>>();
            GameNo = ++Game;
        }
        void ReadFile(string filename)
        {
            int player = 0;
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                if (line.StartsWith("Player"))
                {
                    player = int.Parse(line.Substring(7, 1));
                    Decks[player]= new List<int>();
                }
                else if (line == "")
                {
                    continue;
                }
                else
                {
                    Decks[player].Add(int.Parse(line));
                }
            }
        }
        public int Other(int player)
        {
            return 3 - player;
        }
        public void Play()
        {
            while (Decks.All(d => d.Value.Count() > 0))
            {
                if (Decks[1][0] > Decks[2][0])
                {
                    Winner = 1;
                }
                else
                {
                    Winner = 2;
                }
                Decks[Winner].Add(Decks[Winner][0]);
                Decks[Winner].RemoveAt(0);
                Decks[Winner].Add(Decks[Other(Winner)][0]);
                Decks[Other(Winner)].RemoveAt(0);
            }
        }
        bool SeenBefore()
        {

            List<List<int>> decks = Decks.Values.ToList();
            bool seen = Seen.Any(l => Enumerable.SequenceEqual(l[0], decks[0]) && Enumerable.SequenceEqual(l[1], decks[1]));
            List<List<int>> copy = new List<List<int>> { new List<int>(decks[0].ToArray()), new List<int>(decks[1].ToArray()) };
            Seen.Add(copy);
            return seen;
        }
        public int PlayRecursive()
        {
            while (Decks.All(d => d.Value.Count() > 0))
            {
                if (SeenBefore())
                {
                    return 1;
                }
                Dictionary<int, int> cards = new Dictionary<int, int> { { 1, Decks[1][0] }, { 2, Decks[2][0] } };
                Decks[1].RemoveAt(0);
                Decks[2].RemoveAt(0);
                if (Decks[1].Count >= cards[1] && Decks[2].Count >= cards[2])
                {
                    Dictionary<int, List<int>> subdecks = new Dictionary<int, List<int>>();
                    for (int i = 1; i < 3; i++)
                    {
                        subdecks[i] = new List<int>(Decks[i].GetRange(0, cards[i]).ToArray());
                    }
                    Combat subgame = new Combat(subdecks);
                    Winner = subgame.PlayRecursive();
                }
                else if (cards[1] > cards[2])
                {
                    Winner = 1;
                }
                else
                {
                    Winner = 2;
                }
                Decks[Winner].Add(cards[Winner]);
                Decks[Winner].Add(cards[Other(Winner)]);
            }
            return Winner;
        }
        public int WinnerScore()
        {
            int score = 0;
            int n = Decks[Winner].Count;
            for (int i = 0; i < n; i++)
            {
                score += (Decks[Winner][i] * (n - i));
            }
            return score;
        }
    }
}