using System.Collections.Generic;

namespace Advent2020
{
    public class Toboggan
    {
        private List<string> Slope;
        private int RowLength;
        public Toboggan(string filename)
        {
            Slope = FileReader.ReadFileOfStrings(filename);
            RowLength = Slope[0].Length;
        }
        public int Collisions(int right, int down)
        {
            int collisions = 0;
            int col = 0;
            for (int row = 0; row < Slope.Count; row += down)
            {
                if (Slope[row][col] == '#')
                {
                    collisions++;
                }
                col = (col + right) % RowLength;
            }
            return collisions;
        }
        public long CollisionProduct(int [,] slopes)
        {
            long product = 1;
            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                product *= Collisions(slopes[i, 0], slopes[i, 1]);
            }
            return product;
        }
    }
}