using Xunit;
namespace Advent2020
{
    public class MathHomeworkTest
    {
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [InlineData("11 + (2 * 3) + (4 * (5 + 6))", 61)]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void TestEvaluate(string expression, int expected)
        {
            MathHomework homework = new MathHomework();
            Assert.Equal(expected, homework.Evaluate(expression));
        }
        [Fact]
        public void TestSumOfExpressionsInFile()
        {
            MathHomework homework = new MathHomework();
            Assert.Equal(26 + 437 + 12240 + 13632, homework.SumOfExpressionsFromFile("input/day18.txt"));
        }
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("11 + (2 * 3) + (4 * (5 + 6))", 61)]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void TestEvaluatePrecedence(string expression, int expected)
        {
            MathHomeworkWithPrecedence homework = new MathHomeworkWithPrecedence();
            Assert.Equal(expected, homework.Evaluate(expression));
        }
        [Fact]
        public void TestSumOfExpressionsWithPrecedenceInFile()
        {
            MathHomeworkWithPrecedence homework = new MathHomeworkWithPrecedence();
            Assert.Equal(46 + 1445 + 669060 + 23340, homework.SumOfExpressionsFromFile("input/day18.txt"));
        }
    }
}