﻿using System;
using System.Diagnostics;

namespace Advent2020
{
    abstract class Day
    {
        protected string answer_part1 { get; set; }
        protected string answer_part2 { get; set; }

        protected Stopwatch stopwatch;
        protected int dayOfMonth;

        public Day()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public abstract Day Run();

        public void Print()
        {
            stopwatch.Stop();
            Console.WriteLine($"* Day {dayOfMonth}");
            Console.WriteLine($"  Part 1: {answer_part1}");
            Console.WriteLine($"  Part 2: {answer_part2}");
            Console.WriteLine($"  Finished in {stopwatch.ElapsedMilliseconds} ms"); 
            Console.WriteLine();    
        }
    }

    class Day1 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 1;
            var expense = new ExpenseReport();
            answer_part1 = expense.MultiplyTwoEntriesFromFile("input/day1.txt").ToString();
            answer_part2 = expense.MultiplyThreeEntriesFromFile("input/day1.txt").ToString();
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent 2020\n");
            new Day1().Run().Print();

        }
    }
}
