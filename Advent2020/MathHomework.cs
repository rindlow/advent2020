    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    namespace Advent2020
    {
        public enum TokenType
        {
            Operator,
            Operand
        }
        public enum Operator
        {
            Add,
            Mul,
            StartParen,
            EndParen
        }
        public class MathToken
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
        public class ShuntingYard
        {
            private List<MathToken> Output;
            private List<MathToken> OpStack;
            public ShuntingYard()
            {
                Output = new List<MathToken>();
                OpStack = new List<MathToken>();
            }
            private void PopOperatorsToOutput(bool precedence, MathToken token)
            {
                while (OpStack.Count > 0
                        && OpStack[0].Operator != Operator.StartParen
                        && !(precedence && token.Operator == Operator.Add && OpStack[0].Operator == Operator.Mul))
                {
                    Output.Add(OpStack[0]);
                    OpStack.RemoveAt(0);
                }
            }
            public List<MathToken> InfixToPostfix (bool precedence, List<MathToken> infix)
            {
                Output.Clear();
                OpStack.Clear();
                foreach (MathToken token in infix)
                {
                    if (token.Type == TokenType.Operand)
                    {
                        Output.Add(token);
                    }
                    else if (token.Operator == Operator.StartParen)
                    {
                        OpStack.Insert(0, token);
                    }
                    else if (token.Operator == Operator.EndParen) 
                    {
                        PopOperatorsToOutput(precedence, token);
                        if (OpStack[0].Operator == Operator.StartParen)
                        {
                            OpStack.RemoveAt(0);
                        }
                        else
                        {
                            throw new Exception("Mismatched parenthesis");
                        }
                    }
                    else
                    {
                        PopOperatorsToOutput(precedence, token);
                        OpStack.Insert(0, token);
                    }
                }
                MathToken end = new MathToken();
                end.Type = TokenType.Operator;
                end.Operator = Operator.EndParen;
                PopOperatorsToOutput(precedence, end);
                return Output;
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
            public long Evaluate (bool precedence, string input)
            {
                List<long> stack = new List<long>();
                foreach (MathToken token in InfixToPostfix(precedence, Tokenize(input)))
                {
                    if (token.Type == TokenType.Operand)
                    {
                        stack.Insert(0, token.Operand);
                    }
                    else if (token.Operator == Operator.Add)
                    {
                        stack[1] += stack[0];
                        stack.RemoveAt(0);
                    }
                    else if (token.Operator == Operator.Mul)
                    {
                        stack[1] *= stack[0];
                        stack.RemoveAt(0);
                    }
                }
                return stack.Single();
            }
            public long SumOfExpressionsFromFile(bool precedence, string filename)
            {
                return FileReader.ReadFileOfStrings(filename).Select(expression => Evaluate(precedence, expression)).Sum();
            }
        }
    }