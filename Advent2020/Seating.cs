using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020
{
    public class Seating
    {
        private List<List<char>> Seats;
        private int Rows;
        private int Cols;
        public Seating(string filename)
        {
            Seats = FileReader.ReadFileOfStrings(filename).Select(s => s.ToList<char>()).ToList();
            Rows = Seats.Count;
            Cols = Seats[0].Count;
        }
        public void RunUntilNoChange()
        {
            while (UpdateSeats((row, col) => Seats[row][col] == 'L' && OccupiedInLineOfSight(row, col, 1) == 0, '#')
                   || UpdateSeats((row, col) => Seats[row][col] == '#' && OccupiedInLineOfSight(row, col, 1) >= 4, 'L'));
        }
        public void RunLineOfSightUntilNoChange()
        {
            while (UpdateSeats((row, col) => Seats[row][col] == 'L' && OccupiedInLineOfSight(row, col, 0) == 0, '#')
                   || UpdateSeats((row, col) => Seats[row][col] == '#' && OccupiedInLineOfSight(row, col, 0) >= 5, 'L'));
        }
        private bool UpdateSeats(Func<int, int, bool> test, char newStatus)
        {
            int changes = 0;
            List<List<char>> newSeats = new List<List<char>>();
            for (int row = 0; row < Rows; row++)
            {
                List<char> newRow = new List<char>();
                for (int col = 0; col < Cols; col++)
                {   
                    if (test(row, col))
                    {
                        newRow.Add(newStatus);
                        changes++;
                    }
                    else
                    {
                        newRow.Add(Seats[row][col]);
                    }
                }
                newSeats.Add(newRow);
            }      
            Seats = newSeats;
            return changes > 0;
        }
        private int OccupiedInLineOfSight(int row, int col, int maxSight)
        {
            int occupied = 0;
            for (int dr = -1; dr < 2; dr++)
            {
                for (int dc = -1; dc < 2; dc++)
                {
                    if (dr == 0 && dc == 0)
                    {
                        continue;
                    }
                    int r = row;
                    int c = col;
                    int sight = 0;
                    while (maxSight == 0 || sight < maxSight)
                    {
                        sight++;
                        r += dr;
                        c += dc;
                        if (r < 0 || r >= Rows || c < 0 || c >= Cols)
                        {
                            break;
                        }
                        if (Seats[r][c] == '#')
                        {
                            occupied++;
                            break;
                        }
                        if (Seats[r][c] == 'L')
                        {
                            break;
                        }
                    }
                }
            }
            return occupied;
        }
        public int OccupiedSeats()
        {
            return Seats.Select(row => row.Count(c => c == '#')).Sum();
        }
    }
}
