using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace Advent2020.Lobby
{
    public class Floor
    {
        public Dictionary<(int, int), bool> Tiles;
        public Floor()
        {
            Tiles = new Dictionary<(int, int), bool>();
        }
        public void FlipTilesFromFile(string filename)
        {

            Regex neighbourRe = new Regex(@"e|se|sw|w|nw|ne");
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                int x = 0;
                int y = 0;
                foreach (Match neighbour in neighbourRe.Matches(line))
                {
                    switch (neighbour.Groups[0].Value)
                    {
                    case "e":
                        x++;
                        break;
                    case "se":
                        x++;
                        y++;
                        break;
                    case "sw":
                        y++;
                        break;
                    case "w":
                        x--;
                        break;
                    case "nw":
                        x--;
                        y--;
                        break;
                    case "ne":
                        y--;
                        break;
                    }
                }
                if (Tiles.ContainsKey((x, y)))
                {
                    Tiles[(x, y)] = !Tiles[(x, y)];
                }
                else
                {
                    Tiles[(x, y)] = true;
                }
            }
        }
        List<(int, int)> Neighbours(int x, int y)
        {
            return new List<(int, int)> { (x+1, y), (x+1,y+1), (x, y+1), (x-1, y), (x-1, y-1), (x, y-1)};
        }
        int BlackNeighbours(int x, int y)
        {
            return Neighbours(x, y).Where(t => Tiles.ContainsKey(t) && Tiles[t]).Count();
        }
        public bool ShouldBeBlack(int x, int y)
        {
            int bn = BlackNeighbours(x, y);
            bool isBlack = false;
            if (Tiles.ContainsKey((x, y)))
            {
                isBlack = Tiles[(x, y)];
            }

            if (isBlack && (bn == 0 || bn > 2))
            {
                return false;
            }
            else if (!isBlack && bn == 2)
            {
                return true;
            }
            else
            {
                return isBlack;
            }   
        }
        public void ApplyRules()
        {
            Dictionary<(int, int), bool> newTiles = new Dictionary<(int, int), bool>();
            foreach (KeyValuePair<(int, int), bool> kvp in Tiles)
            {
                (int x, int y) = kvp.Key;
                newTiles[(x, y)] = ShouldBeBlack(x, y);
                if (kvp.Value)
                {
                    foreach ((int nx, int ny) in Neighbours(x, y))
                    {
                        if (!Tiles.ContainsKey((nx, ny)) && ShouldBeBlack(nx ,ny))
                        {
                            newTiles[(nx, ny)] = true;
                        }
                    }
                }
            }
            Tiles = newTiles;
        }
        public void ApplyRulesForDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                ApplyRules();
                // Console.WriteLine($"Day {i + 1}: {NumberOfBlackTiles()}");
            }
        }
        public int NumberOfBlackTiles()
        {
            return Tiles.Values.Where(t => t).Count();
        }
    }
}