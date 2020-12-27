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
        public T After(T item)
        {
            return Elements[(Elements.IndexOf(item) + 1) % Elements.Count];
        }
        public List<T> RemoveAfter(T item, int count)
        {
            // Console.WriteLine($"RemoveAfter({item}, {count}");
            List<T> res = new List<T>();
            int index = Elements.IndexOf(item) + 1;
            // Console.WriteLine($".IndexOf({item}) = {Elements.IndexOf(item)}, index = {index}");
            while (res.Count < count)
            {
                index %= Elements.Count;
                res.Add(Elements[index]);
                Elements.RemoveAt(index);
            }
            return res;
        }
        public void InsertAfter(T item, List<T> elements)
        {
            Elements.InsertRange((Elements.IndexOf(item) + 1) % Elements.Count, elements);
        }
    }
    class RingElement<T>
    {
        public T Value { get; }
        public RingElement<T> Next { get; set; }
        public RingElement(T value)
        {
            Value = value;
            Next = null;
        }
    }
    class FastRing<T>
    {
        Dictionary<T, RingElement<T>> Index;
        public int Count
        {
            get
            {
                return Index.Count;
            }
        }
        public FastRing(IEnumerable<T> input)
        {
            Index = new Dictionary<T, RingElement<T>>();
            RingElement<T> firstElement = null;
            RingElement<T> lastElement = null;
            foreach (T item in input)
            {
                RingElement<T> element = new RingElement<T>(item);
                Index[item] = element;
                if (firstElement == null)
                {
                    firstElement = element;
                }
                if (lastElement != null)
                {
                    lastElement.Next = element;
                }
                lastElement = element;
            }
            lastElement.Next = firstElement;
        }
        public T After(T item)
        {
            return Index[item].Next.Value;
        }
        public List<T> RemoveAfter(T item, int count)
        {
            List<T> res = new List<T>();
            RingElement<T> cur = Index[item];
            while (res.Count < count)
            {
                T value = cur.Next.Value;
                res.Add(value);
                cur.Next = cur.Next.Next;
                Index.Remove(value);
            }
            return res;
        }
        public void InsertAfter(T item, List<T> elements)
        {
            RingElement<T> cur = Index[item];
            foreach (T value in elements)
            {
                RingElement<T> element = new RingElement<T>(value);
                Index[value] = element;
                element.Next = cur.Next;
                cur.Next = element;
                cur = element;
            }
        }
    }
    public class Cups
    {
        FastRing<int> Circle;
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
            // Console.WriteLine($"#labels = {labels.Count}");
            Circle = new FastRing<int>(labels);
            Lowest = labels.Min();
            Highest = labels.Max();
            // Console.WriteLine($"Lowest = {Lowest} Highest = {Highest}");
            Current = labels[0];
        }
        void Move()
        {
            // Console.WriteLine($"cups: {Circle}");
            List<int> removed = Circle.RemoveAfter(Current, 3);
            // Console.WriteLine($"pick up: {string.Join(", ", removed)}");
            int destination = Current - 1;
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
            Circle.InsertAfter(destination, removed);
            Current = Circle.After(Current);
            // Console.WriteLine();
        }
        public string LabelAfterMoves(int moves)
        {
            for (int i = 0; i < moves; i++)
            {
                Move();
            }
            return string.Join("", Circle.RemoveAfter(1, Circle.Count - 1).Select(i => i.ToString()));
        }
        public long ProductOfFirstCups(int moves)
        {
            // Circle.Insert(Circle.Count, Enumerable.Range(Circle.Count + 1, 1000000 - Circle.Count).ToList());
            // Current = Circle[0];
            for (int i = 0; i < moves; i++)
            {
                // if (i % 1000 == 0)
                // {
                //     Console.WriteLine(i);
                // }
                Move();
            }
            List<int> firstPair = Circle.RemoveAfter(1, 2);
            long first = firstPair[0];
            long second = firstPair[1];
            // Console.WriteLine($"first = {first} second = {second}");
            return first * second;
        }
    }
}