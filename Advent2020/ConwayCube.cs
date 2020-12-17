using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020
{
    public class ConwayCube
    {
        Dictionary<Tuple<int, int, int, int>, bool> Grid;
        int Xmax;
        int Xmin;
        int Ymax;
        int Ymin;
        int Zmax;
        int Zmin;
        int Wmax;
        int Wmin;
        public ConwayCube(string filename)
        {
            Grid = new Dictionary<Tuple<int, int, int, int>, bool>();
            ReadFile(filename);
            UpdateBounds();
        }
        private void ReadFile(string filename)
        {
            int y = 0;
            foreach (string line in FileReader.ReadFileOfStrings(filename))        
            {
                for (int x = 0; x < line.Length; x++)
                {
                    Grid[new Tuple<int, int, int, int>(x, y, 0, 0)] = line[x] == '#';
                }
                y++;
            }
        }
        private void UpdateBounds()
        {
            Xmin = Grid.Keys.Select(t => t.Item1).Min();
            Xmax = Grid.Keys.Select(t => t.Item1).Max();
            Ymin = Grid.Keys.Select(t => t.Item2).Min();
            Ymax = Grid.Keys.Select(t => t.Item2).Max();
            Zmin = Grid.Keys.Select(t => t.Item3).Min();
            Zmax = Grid.Keys.Select(t => t.Item3).Max();
            Wmin = Grid.Keys.Select(t => t.Item4).Min();
            Wmax = Grid.Keys.Select(t => t.Item4).Max();
        }
        private bool IsActive(int x, int y, int z, int w)
        {
            Tuple<int, int, int, int> coord = new Tuple<int, int, int, int>(x, y, z, w);
            return Grid.ContainsKey(coord) && Grid[coord];
        }
        private void PrintGrid()
        {
            for (int w = Wmin; w <= Wmax; w++)
            {
                for (int z = Zmin; z <= Zmax; z++)
                {
                    Console.WriteLine($"z={z}, w={w}");
                    for (int y = Ymin; y <= Ymax; y++)
                    {
                        for (int x = Xmin; x <= Xmax; x++)
                        {
                            if (IsActive(x, y, z, w))
                            {
                                Console.Write("#");
                            }
                            else
                            {
                                Console.Write(".");
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        private int NumberOfNeighbours(int x, int y, int z, int w)
        {
            int num = 0;
            for (int nx = x - 1; nx < x + 2; nx++)
            {
                for (int ny = y - 1; ny < y + 2; ny++)
                {
                    for (int nz = z - 1; nz < z + 2; nz++)
                    {
                        for (int nw = w - 1; nw < w + 2; nw++)
                        {
                            if (nx == x && ny == y && nz == z && nw == w)
                            {
                                continue;
                            }
                            if (IsActive(nx, ny, nz, nw))
                            {
                                num++;
                            }
                        }
                    }
                }
            }
            return num;
        }
        private void Step(int dim)
        {
            Dictionary<Tuple<int, int, int, int>, bool> newGrid = new Dictionary<Tuple<int, int, int, int>, bool>();
            int minW = 0;
            int maxW = 0;
            if (dim == 4)
            {
                minW = Wmin - 1;
                maxW = Wmax + 1;
            }
            for (int x = Xmin - 1; x <= Xmax + 1; x++)
            {
                for (int y = Ymin - 1; y <= Ymax + 1; y++)
                {
                    for (int z = Zmin - 1; z <= Zmax + 1; z++)
                    {
                        for (int w = minW; w <= maxW; w++)
                        {
                            int non = NumberOfNeighbours(x, y, z, w);
                            Tuple<int, int, int, int> coord = new Tuple<int, int, int, int>(x, y, z, w);
                            newGrid[coord] = (non == 3 || (non == 2 && IsActive(x, y, z, w)));
                        }
                    }
                }
            }
            Grid = newGrid;
            UpdateBounds();
        }
        public void Run(int steps, int dim)
        {
            for (int i = 0; i < steps; i++)
            {
                // Console.WriteLine($"\nAfter {i} cycles");
                // PrintGrid();
                Step(dim);
            }
            // PrintGrid();
        }
        public int NumberOfActiveCubes()
        {
            return Grid.Values.Where(active => active).Count();
        }
    }
}