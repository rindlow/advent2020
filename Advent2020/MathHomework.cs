using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Advent2020
{
    enum TokenType
    {
        Operator,
        Operand
    }
    enum Operator
    {
        Add,
        Mul,
        StartParen,
        EndParen
    }
    class MathToken
    {
        public TokenType Type { get; set; }
        public Operator Operator { get; set; }
        public long Operand { get; set; }
        public override string ToString()
        {
            if (Type == TokenType.Operator)
            {
                switch (Operator)
                {
                case Operator.StartParen:
                    return "(";
                case Operator.EndParen:
                    return ")";
                case Operator.Add:
                    return "+";
                case Operator.Mul:
                    return "*";
                }
            }
            return Operand.ToString();
        }
    }
    public class MathHomeworkWithPrecedence
    {
        Regex AddRe;
        Regex MulRe;
        Regex OpRe;
        Regex ParRe;
        public MathHomeworkWithPrecedence()
        {
            AddRe = new Regex(@"(\d+) \+ (\d+)");
            MulRe = new Regex(@"(\d+) \* (\d+)");
            OpRe = new Regex(@"(\d+) ([+*]) (\d+)");
            ParRe = new Regex(@"\(([^()]+)\)");
        }
        public static string MatchAdd(Match match)
        {
            long lhs = Int64.Parse(match.Groups[1].Value);
            long rhs = Int64.Parse(match.Groups[2].Value);
            return $"{lhs + rhs}";
        }
        public static string MatchMul(Match match)
        {
            long lhs = Int64.Parse(match.Groups[1].Value);
            long rhs = Int64.Parse(match.Groups[2].Value);
            return $"{lhs * rhs}";
        }
        public static string MatchOp(Match match)
        {
            long lhs = Int64.Parse(match.Groups[1].Value);
            long rhs = Int64.Parse(match.Groups[3].Value);
            if (match.Groups[2].Value == "+")
            {
                return $"{lhs + rhs}";
            }
            return $"{lhs * rhs}";
        }
        public string MatchPar(Match match)
        {
            long res = Evaluate(match.Groups[1].Value);
            return res.ToString();
        }
        public long Evaluate(string input)
        {
            string oldString = input;
            while (true)
            {
                string addedString = AddRe.Replace(oldString, MatchAdd);
                string deparenizedString = ParRe.Replace(addedString, MatchPar);
                if (deparenizedString == oldString)
                {
                    break;
                }
                oldString = deparenizedString;
            }
            string newString = MulRe.Replace(oldString, MatchMul);
            long res;
            if (Int64.TryParse(newString, out res))
            {
                return res;
            }
            return Evaluate(newString);
        }
        public long SumOfExpressionsFromFile(string filename)
        {
            return FileReader.ReadFileOfStrings(filename).Select(line => Evaluate(line)).Sum();
        }
    }
    public class MathHomework
    {
        List<MathToken> Stack;
        public MathHomework()
        {
            Stack = new List<MathToken>();
        }

        private List<MathToken> Tokenize(string input)
        {
            List<MathToken> tokens = new List<MathToken>();
            Regex TokenRe = new Regex(@"\s*(\d+|\+|\*|\(|\))");
            foreach (Match match in TokenRe.Matches(input))
            {
                MathToken token = new MathToken();
                switch (match.Groups[1].Value)
                {
                case "(":
                    token.Type = TokenType.Operator;
                    token.Operator = Operator.StartParen;
                    break;
                case ")":
                    token.Type = TokenType.Operator;
                    token.Operator = Operator.EndParen;
                    break;
                case "+":
                    token.Type = TokenType.Operator;
                    token.Operator = Operator.Add;
                    break;
                case "*":
                    token.Type = TokenType.Operator;
                    token.Operator = Operator.Mul;
                    break;
                default:
                    token.Type = TokenType.Operand;
                    token.Operand = int.Parse(match.Groups[1].Value);
                    break;
                }
                tokens.Add(token);
            }
            return tokens;
        }
        private void PrintStack()
        {
            for (int i = 0; i < Stack.Count; i++)
            {
                Console.WriteLine($"  {i}: {Stack[i]}");
            }
        }
        private bool ApplyOp()
        {
            long rhs;
            if (Stack.Count > 2
                && Stack[0].Type == TokenType.Operand
                && Stack[1].Type == TokenType.Operator
                && Stack[2].Type == TokenType.Operand)
                {
                    switch (Stack[1].Operator)
                    {
                    case Operator.Add:
                        rhs = Stack[0].Operand;
                        Stack.RemoveAt(0);
                        Stack.RemoveAt(0);
                        Stack[0].Operand += rhs;
                        return true;
                    case Operator.Mul:
                        rhs = Stack[0].Operand;
                        Stack.RemoveAt(0);
                        Stack.RemoveAt(0);
                        Stack[0].Operand *= rhs;
                        return true;
                    default:
                        return false;
                    }
                }
            return false;
        }
        public long Evaluate(string input)
        {
            foreach (MathToken token in Tokenize(input))
            {
                if (token.Type == TokenType.Operand)
                {
                    Stack.Insert(0, token);  
                }
                else
                {
                    switch (token.Operator)
                    {
                    case Operator.Add:
                    case Operator.Mul:
                    case Operator.StartParen:
                        Stack.Insert(0, token);
                        break;
                    case Operator.EndParen:
                        if (Stack.Count > 1
                            && Stack[0].Type == TokenType.Operand
                            && Stack[1].Type == TokenType.Operator && Stack[1].Operator == Operator.StartParen)
                        {
                            Stack.RemoveAt(1);
                            ApplyOp();
                        }
                        else
                        {
                            throw new Exception("too few entries on stack");
                        }
                        break;
                    }
                }
                ApplyOp();
            }
            long res = Stack[0].Operand;
            Stack.RemoveAt(0);
            return res;
        }
        public long SumOfExpressionsFromFile(string filename)
        {
            return FileReader.ReadFileOfStrings(filename).Select(line => Evaluate(line)).Sum();
        }
    }
}