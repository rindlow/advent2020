using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Advent2020.ImageTiles
{
    class Tile
    {
        public int Id { get; set; }
        public List<string> Rows { get; set; }
        public HashSet<int> BorderMatches { get; set; }
        public List<int> Borders { get; set; }
        public Tile()
        {
            Rows = new List<string>();
            BorderMatches = new HashSet<int>();
            Borders = new List<int>();
        }
        public void Print()
        {
            Console.WriteLine($"Tile {Id}");
            foreach (string row in Rows)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }
        int CharArrayToInt(char[] array)
        {
            string binary = new string(array).Replace('#', '1').Replace('.', '0');
            return Convert.ToInt32(binary, 2);
        }
        public void FindBorders()
        {
            char[] edge;
            Borders.Clear();
            Borders.Add(CharArrayToInt(Rows[0].ToArray()));
            Borders.Add(CharArrayToInt(Rows[0].Reverse().ToArray()));

            edge = Rows.Select(row => row[0]).ToArray();
            Borders.Add(CharArrayToInt(edge));
            Borders.Add(CharArrayToInt(edge.Reverse().ToArray()));

            Borders.Add(CharArrayToInt(Rows[Rows.Count - 1].ToArray()));
            Borders.Add(CharArrayToInt(Rows[Rows.Count - 1].Reverse().ToArray()));

            edge = Rows.Select(row => row[row.Length - 1]).ToArray();
            Borders.Add(CharArrayToInt(edge));
            Borders.Add(CharArrayToInt(edge.Reverse().ToArray()));
        }
        public void Match(int tile)
        {
            if (tile != Id)
            {
                BorderMatches.Add(tile);
            }
        }
        public void RotateCW()
        {
            List<string> newRows = new List<string>();
            for (int i = 0; i < Rows[0].Length; i++)
            {
                newRows.Add(new string(Rows.Select(row => row[i]).Reverse().ToArray()));
            }
            Rows = newRows;
            FindBorders();
        }
        public void RotateCCW()
        {
            List<string> newRows = new List<string>();
            for (int i = Rows[0].Length - 1; i >= 0; i--)
            {
                newRows.Add(new string(Rows.Select(row => row[i]).ToArray()));
            }
            Rows = newRows;
            FindBorders();
        }
        public void FlipHorizontal()
        {
            Rows.Reverse();
            FindBorders();
        }
        public void FlipVertical()
        {
            List<string> newRows = new List<string>();
            foreach (string row in Rows)
            {
                char[] array = row.ToCharArray();
                Array.Reverse(array);
                newRows.Add(new string(array));
            }
            Rows = newRows;
            FindBorders();
        }
    }
    public class Image
    {
        Dictionary<int, Tile> Tiles;
        List<string> Rows;
        public Image(string filename)
        {
            Tiles = new Dictionary<int, Tile>();
            Rows = new List<string>();
            ReadFile(filename);
        }
        void ReadFile(string filename)
        {
            Tile tile = new Tile();
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                if (line.StartsWith("Tile"))
                {
                    tile.Id = int.Parse(line.Substring(5, line.Length - 5 - 1));
                }
                else if (line == "")
                {
                    tile.FindBorders();
                    Tiles[tile.Id] = tile;
                    tile = new Tile();
                }
                else
                {
                    tile.Rows.Add(line);
                }
            }
            tile.FindBorders();
            Tiles[tile.Id] = tile;
        }
        public long ProductOfCornerTiles()
        {
            return FindCornerTiles().Aggregate(1L, (a, b) => a * b, a => a);
        }
        List<int> FindCornerTiles()
        {
            Dictionary<int, HashSet<int>> BorderTiles = new Dictionary<int, HashSet<int>>();
            foreach (Tile tile in Tiles.Values)
            {
                foreach (int border in tile.Borders)
                {
                    if (BorderTiles.ContainsKey(border))
                    {
                        BorderTiles[border].Add(tile.Id);
                    }
                    else
                    {
                        BorderTiles[border] = new HashSet<int> { tile.Id };
                    }
                }
            }
            foreach (HashSet<int> tiles in BorderTiles.Values)
            {
                foreach (int tile in tiles)
                {
                    foreach (int tile2 in tiles)
                    {
                        Tiles[tile].Match(tile2);
                    }
                }
            }
            return Tiles.Values.Where(t => t.BorderMatches.Count == 2).Select(t => t.Id).ToList();
        }
        int CommonBorder(int tile1, int tile2)
        {
            HashSet<int> set = new HashSet<int>(Tiles[tile1].Borders);
            set.IntersectWith(Tiles[tile2].Borders);
            return set.First();
        }
        List<Tile> TileRow(Tile startTile)
        {
            List<Tile> tilerow = new List<Tile>();
            int side = (int)Math.Sqrt(Tiles.Count);
            tilerow.Add(startTile);
            Tile lastTile = startTile;
            // lastTile.Print();
            for (int i = 1; i < side; i++)
            {
                int rightBorder = lastTile.Borders[6];
                Tile nextTile = Tiles[Tiles.Values.Where(t => t.Borders.Contains(rightBorder) && t.Id != lastTile.Id).Select(t => t.Id).First()];
                int index = nextTile.Borders.IndexOf(rightBorder);
                switch(index)
                {
                case 0:
                    nextTile.RotateCCW();
                    nextTile.FlipHorizontal();
                    break;
                case 1:
                    nextTile.RotateCCW();
                    break;
                case 3:
                    nextTile.FlipHorizontal();
                    break;
                case 2:
                    // Perfect!
                    break;
                case 4:
                    nextTile.RotateCW();
                    break;
                case 5:
                    nextTile.RotateCW();
                    nextTile.FlipHorizontal();
                    break;
                case 6:
                    nextTile.FlipVertical();
                    break;
                case 7:
                    nextTile.FlipHorizontal();
                    nextTile.FlipVertical();
                    break;
                }
                tilerow.Add(nextTile);
                lastTile = nextTile;
            }
            return tilerow;
        }
        Tile TileBelow(Tile above)
        {
            int bottomBorder = above.Borders[4];
            Tile nextTile = Tiles[Tiles.Values.Where(t => t.Borders.Contains(bottomBorder) && t.Id != above.Id).Select(t => t.Id).First()];
            int index = nextTile.Borders.IndexOf(bottomBorder);
            switch(index)
            {
            case 0:
                // Perfect!
                break;
            case 1:
                nextTile.FlipVertical();
                break;
            case 2:
                nextTile.RotateCW();
                nextTile.FlipVertical();
                break;
            case 3:
                nextTile.RotateCW();
                break;
            case 4:
                nextTile.FlipHorizontal();
                break;
            case 5:
                nextTile.FlipHorizontal();
                nextTile.FlipVertical();
                break;
            case 6:
                nextTile.RotateCCW();
                break;
            case 7:
                nextTile.RotateCCW();
                nextTile.FlipVertical();
                break;
            }
            return nextTile;
        }
        List<string> TileRowToImage(List<Tile> TileRow)
        {
            int side = TileRow[0].Rows.Count;
            List<string> image = new List<string>();
            for (int row = 1; row < side - 1; row++)
            {
                image.Add(String.Join("", TileRow.Select(t => t.Rows[row].Substring(1, side - 2))));
            }
            return image;
        }
        List<string> TileImage()
        {
            List<List<Tile>> tilesquare = new List<List<Tile>>();
            int side = (int)Math.Sqrt(Tiles.Count);
            List<int> cornerTiles = FindCornerTiles();
            Random rnd = new Random();
            Tile startTile = Tiles[FindCornerTiles().First()];
            List<int> commonBorders = startTile.BorderMatches.Select(m => CommonBorder(startTile.Id, m)).ToList();

            while (!commonBorders.Select(b => startTile.Borders.IndexOf(b)).All(i => i > 3))
            {
                startTile.RotateCCW();
            }
            for (int i = 0; i < side; i++)
            {
                List<Tile> row = TileRow(startTile);
                tilesquare.Add(row);
                if (i < side - 1)
                {
                    startTile = TileBelow(row[0]);
                }
            }
            // string red = "\u001b[31m";
            // string black = "\u001b[0m";
            // foreach (List<Tile> row in tilesquare)
            // {
            //     for (int i = 0; i < row[0].Rows.Count; i++)
            //     {
            //         string color;
            //         if (i == 0 || i == row[0].Rows.Count - 1)
            //         {
            //             color = red;
            //         }
            //         else
            //         {
            //             color = black;
            //         }
            //         foreach (Tile tile in row)
            //         {
            //             Console.Write($"  {red}{tile.Rows[i][0]}{color}{tile.Rows[i].Substring(1, tile.Rows[i].Length - 2)}{red}{tile.Rows[i][tile.Rows[i].Length - 1]}{color}");
            //         }
            //         Console.WriteLine();
            //     }
            //     Console.WriteLine(black);
            // }
            List<string> image = new List<string>();
            foreach (List<Tile> tilerow in tilesquare)
            {
                image.AddRange(TileRowToImage(tilerow));
            }
            // foreach (string line in image)
            // {
            //     Console.WriteLine(line);
            // }
            return image;
        }
        bool FindMonster(List<string> image, int row, int col)
        {
            List<(int, int)> monster = new List<(int, int)> { (0,18),(1,0),(1,5),(1,6),(1,11),(1,12),(1,17),(1,18),(1,19),(2,1),(2,4),(2,7),(2,10),(2,13),(2,16) };

            foreach ((int, int) pos in monster)
            {
                if (image[row + pos.Item1][col + pos.Item2] != '#')
                {
                    return false;
                }
            }
            return true;
        }
        int FindMonsters(List<string> image)
        {
            int width = image[0].Length;
            int nMatches = 0;
            for (int row = 0; row < width - 2; row++)
            {
                for (int col = 0; col < width - 19; col++)
                {
                    if (FindMonster(image, row, col))
                    {
                        nMatches++;
                    }
                }
            }
            return nMatches;
        }

        int FlipAndFindMonsters(List<string> image)
        {
            int tries = 0;
            HashSet<string> seen = new HashSet<string>();
            int nMatches = FindMonsters(image);

            while (nMatches == 0 && tries < 8)
            {
                if (tries % 4 == 0)
                {
                    // Console.WriteLine("FlipHorizontal");
                    image.Reverse();
                }
                else
                {
                    // Console.WriteLine("RotateCCW");
                    List<string> newRows = new List<string>();
                    for (int i = image[0].Length - 1; i >= 0; i--)
                    {
                        newRows.Add(new string(image.Select(row => row[i]).ToArray()));
                    }
                    image = newRows;
                }
                nMatches = FindMonsters(image);
                seen.Add(string.Join("", image));
                // Console.WriteLine($"nMatches = {nMatches}");
                tries++;
            }
            // Console.WriteLine($"{seen.Count} images scanned");
            // Console.WriteLine($"{nMatches} monsters found");
            // Console.WriteLine($"{image.Count}x{image[0].Length}");
            return nMatches;
        }
        int NumberOfWaves(List<string> image)
        {
            return image.Select(s => s.Count(c => c == '#')).Sum();
        }
        public int Roughness()
        {
            int now = 0;
            int monsters = 0;

            List<string> image = TileImage();
            now = NumberOfWaves(image);
            monsters = FlipAndFindMonsters(image);            
            return now - 15 * monsters;
        }
    }
}