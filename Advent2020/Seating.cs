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
            while (OccupySeats() || LeaveSeats());
        }
        public void RunLineOfSightUntilNoChange()
        {
            while (OccupySeatsInLineOfSight() || LeaveSeatsInLineOfSight());
        }
        private bool OccupySeats()
        {
            // Console.WriteLine("OccupySeats");
            // PrintSeats();
            int changes = 0;
            List<List<char>> newSeats = new List<List<char>>();
            for (int row = 0; row < Rows; row++)
            {
                List<char> newRow = new List<char>();
                for (int col = 0; col < Cols; col++)
                {   
                    if (Seats[row][col] == 'L' && AdjacentOccupied(row, col) == 0)
                    {
                        newRow.Add('#');
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
        private bool OccupySeatsInLineOfSight()
        {
            // Console.WriteLine("OccupySeats");
            // PrintSeats();
            int changes = 0;
            List<List<char>> newSeats = new List<List<char>>();
            for (int row = 0; row < Rows; row++)
            {
                List<char> newRow = new List<char>();
                for (int col = 0; col < Cols; col++)
                {   
                    if (Seats[row][col] == 'L' && OccupiedInLineOfSight(row, col) == 0)
                    {
                        newRow.Add('#');
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
        private bool LeaveSeats()
        {
            // Console.WriteLine("LeaveSeats");
            // PrintSeats();
            int changes = 0;
            List<List<char>> newSeats = new List<List<char>>();
            for (int row = 0; row < Rows; row++)
            {
                List<char> newRow = new List<char>();
                for (int col = 0; col < Cols; col++)
                {   
                    if (Seats[row][col] == '#' && AdjacentOccupied(row, col) >= 4)
                    {
                        newRow.Add('L');
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
        private bool LeaveSeatsInLineOfSight()
        {
            // Console.WriteLine("LeaveSeats");
            // PrintSeats();
            int changes = 0;
            List<List<char>> newSeats = new List<List<char>>();
            for (int row = 0; row < Rows; row++)
            {
                List<char> newRow = new List<char>();
                for (int col = 0; col < Cols; col++)
                {   
                    if (Seats[row][col] == '#' && OccupiedInLineOfSight(row, col) >= 5)
                    {
                        newRow.Add('L');
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
        private int AdjacentOccupied(int row, int col)
        {
            int occupied = 0;
            for (int r = Math.Max(0, row - 1); r < Math.Min(Rows, row + 2); r++)
            {
                for (int c = Math.Max(0, col - 1); c < Math.Min(Cols, col + 2); c++)
                {
                    if (r == row && c == col)
                    {
                        continue;
                    }
                    if (Seats[r][c] == '#')
                    {
                        occupied++;
                    }
                }
            }
            return occupied;
        }
        private int OccupiedInLineOfSight(int row, int col)
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
                    while (true)
                    {
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
        private void PrintSeats()
        {
            foreach (List<char> row in Seats)
            {
                foreach (char c in row)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}