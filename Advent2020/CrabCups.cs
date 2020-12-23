using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020.CrabCups
{
    public class Ring<T>
    {
        private List<T> Elements;
        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }
        public T this[int index]
        {
            get
            {
                return Elements[index % Elements.Count];
            }
        }
        public Ring(List<T> elements)
        {
            Elements = elements;
        }

        public List<T> Remove(int index, int count)
        {
            List<T> res = new List<T>();
            while (res.Count < count)
            {
                index %= Elements.Count;
                res.Add(Elements[index]);
                Elements.RemoveAt(index);   
            }
            return res;
        }
        public void Insert(int index, List<T> elements)
        {
            Elements.InsertRange(index, elements);
        }
        public int IndexOf(T item)
        {
            return Elements.IndexOf(item);
        }
        public override string ToString()
        {
            return string.Join("  ", Elements);
        }
    }
    public class Cups
    {
        Ring<int> Circle;
        int Current;
        int Lowest;
        int Highest;

        public Cups(string input, int max)
        {
            List<int> labels = input.Select(c => (int)Char.GetNumericValue(c)).ToList();
            for (int i = labels.Max() + 1; i <= max; i++)
            {
                labels.Add(i);
            }
            Console.WriteLine($"#labels = {labels.Count}");
            Circle = new Ring<int>(labels);
            Lowest = labels.Min();
            Highest = labels.Max();
            Console.WriteLine($"Lowest = {Lowest} Highest = {Highest}");
            Current = 0;
        }
        void Move()
        {
            // Console.WriteLine($"cups: {Circle}");
            List<int> removed = Circle.Remove(Circle.IndexOf(Current) + 1, 3);
            // Console.WriteLine($"pick up: {string.Join(", ", removed)}");
            int destination = Circle[Circle.IndexOf(Current)] - 1;
            if (destination < Lowest)
            {
                // Console.WriteLine($"destination under {Lowest}, wrapping to {Highest}");
                destination = Highest;
            }
            while (removed.Contains(destination))
            {
                destination--;
                // Console.WriteLine($"destination decreased to {destination}");
                if (destination < Lowest)
                {
                    // Console.WriteLine($"destination under {Lowest}, wrapping to {Highest}");
                    destination = Highest;
                }
            }
            // Console.WriteLine($"destination: {destination}");
            Circle.Insert(Circle.IndexOf(destination) + 1, removed);
            Current = Circle[Circle.IndexOf(Current) + 1];
            // Console.WriteLine();
        }
        public string LabelAfterMoves(int moves)
        {
            Current = Circle[0];
            for (int i = 0; i < moves; i++)
            {
                Move();
            }
            return string.Join("", Circle.Remove(Circle.IndexOf(1) + 1, Circle.Count - 1).Select(i => i.ToString()));
        }
        public long ProductOfFirstCups()
        {
            Circle.Insert(Circle.Count, Enumerable.Range(Circle.Count + 1, 1000000 - Circle.Count).ToList());
            Current = Circle[0];
            for (int i = 0; i < 10000000; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine(i);
                }
                Move();
            }
            long first = Circle[Circle.IndexOf(1) + 1];
            long second = Circle[Circle.IndexOf(1) + 2];
            Console.WriteLine($"first = {first} second = {second}");
            return first * second;
        }
    }
}