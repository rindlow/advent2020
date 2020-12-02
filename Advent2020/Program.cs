using System;
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
            ExpenseReport expense = new ExpenseReport();
            answer_part1 = expense.MultiplyEntriesFromFile("input/day1.txt", 2, 2020).ToString();
            answer_part2 = expense.MultiplyEntriesFromFile("input/day1.txt", 3, 2020).ToString();
            return this;
        }
    }

    class Day2 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 2;
            answer_part1 = Password.CheckPasswordsFromFile("input/day2.txt").ToString();
            answer_part2 = Password.CheckPasswordsFromFileToboggan("input/day2.txt").ToString();
            return this;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Advent 2020\n");
            Day[] Days = new Day[26];
            Days[1] = new Day1();
            Days[2] = new Day2();

            string[] argv = System.Environment.GetCommandLineArgs();
            if (argv.Length < 2)
            {
                for (int i = 1; i < 26; i++)
                {
                    if (Days[i] != null)
                    {
                        Days[1].Run().Print();
                    }
                }
            }
            else
            {
                for (int i = 1; i < argv.Length; i++)
                {
                    int day;
                    if (int.TryParse(argv[i], out day) && Days[day] != null)
                    {
                        Days[day].Run().Print();
                    }
                }
            }
        }
    }
}
