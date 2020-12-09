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
            stopwatch.Start();

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
            stopwatch.Start();

            answer_part1 = Password.CheckPasswordsFromFile("input/day2.txt").ToString();
            answer_part2 = Password.CheckPasswordsFromFileToboggan("input/day2.txt").ToString();
            return this;
        }
    }
    class Day3 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 3;
            stopwatch.Start();
            Toboggan toboggan = new Toboggan("input/day3.txt");
            answer_part1 = toboggan.Collisions(3, 1).ToString();
            answer_part2 = toboggan.CollisionProduct(new int[,] {{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}}).ToString();
            return this;
        }
    }
    class Day4 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 4;
            stopwatch.Start();
            answer_part1 = Passport.CheckPassportsFromFile("input/day4.txt").ToString();
            answer_part2 = Passport.ValidatePassportsFromFile("input/day4.txt").ToString();
            return this;
        }
    }    
    class Day5 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 5;
            stopwatch.Start();
            answer_part1 = BoardingPass.HighestSeatIdInFile("input/day5.txt").ToString();
            answer_part2 = BoardingPass.FindGapInFile("input/day5.txt").ToString();
            return this;
        }
    }    
    class Day6 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 6;
            stopwatch.Start();
            answer_part1 = CustomsDeclaration.SumDeclarationsFromFile("input/day6.txt").ToString();
            answer_part2 = CustomsDeclaration.SumDeclarationsEveryoneFromFile("input/day6.txt").ToString();
            return this;
        }
    }    
    class Day7 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 7;
            stopwatch.Start();
            LuggageProcess process = new LuggageProcess("input/day7.txt");
            answer_part1 = process.NumberOfContainingBags("shiny gold").ToString();
            answer_part2 = process.NumberOfBagsInBag("shiny gold").ToString();
            return this;
        }
    }
    class Day8 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 8;
            stopwatch.Start();
            GameConsole console = new GameConsole();
            console.ReadProgamFromFile("input/day8.txt");
            answer_part1 = console.RunUntilLoop().ToString();
            answer_part2 = console.RunUntilPatched().ToString();
            return this;
        }
    }
    class Day9 : Day
    {
        public override Day Run()
        {
            dayOfMonth = 9;
            stopwatch.Start();
            Xmas xmas = new Xmas("input/day9.txt", 25);
            answer_part1 = xmas.FirstInvalidNumber().ToString();
            answer_part2 = xmas.FindWeakness().ToString();
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
            Days[3] = new Day3();
            Days[4] = new Day4();
            Days[5] = new Day5();
            Days[6] = new Day6();
            Days[7] = new Day7();
            Days[8] = new Day8();
            Days[9] = new Day9();

            string[] argv = System.Environment.GetCommandLineArgs();
            if (argv.Length < 2)
            {
                for (int i = 1; i < 26; i++)
                {
                    if (Days[i] != null)
                    {
                        Days[i].Run().Print();
                    }
                }
            }
            else
            {
                for (int i = 1; i < argv.Length; i++)
                {
                    int day;
                    if (int.TryParse(argv[i], out day) && day > 0 && day < 26 && Days[day] != null)
                    {
                        Days[day].Run().Print();
                    }
                }
            }
        }
    }
}
