using Xunit;
namespace Advent2020
{
    public class MathHomeworkTest
    {
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", false, 71)]
        [InlineData("11 + (2 * 3) + (4 * (5 + 6))", false, 61)]
        [InlineData("2 * 3 + (4 * 5)", false, 26)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", false, 13632)]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", true, 231)]
        [InlineData("11 + (2 * 3) + (4 * (5 + 6))", true, 61)]
        [InlineData("2 * 3 + (4 * 5)", true, 46)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", true, 23340)]
        public void TestEvaluate(string expression, bool precedence, int expected)
        {
            ShuntingYard homework = new ShuntingYard();
            Assert.Equal(expected, homework.Evaluate(precedence, expression));
        }
        [Fact]
        public void TestSumOfExpressionsInFile()
        {
            ShuntingYard homework = new ShuntingYard();
            Assert.Equal(26 + 437 + 12240 + 13632, homework.SumOfExpressionsFromFile(false, "input/day18.txt"));
        }
      
        [Fact]
        public void TestSumOfExpressionsWithPrecedenceInFile()
        {
            ShuntingYard homework = new ShuntingYard();
            Assert.Equal(46 + 1445 + 669060 + 23340, homework.SumOfExpressionsFromFile(true, "input/day18.txt"));
        }
    }
}