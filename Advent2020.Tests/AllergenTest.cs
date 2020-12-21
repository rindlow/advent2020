using Xunit;
using Advent2020.Allergen;
namespace Advent2020.Tests
{
    public class AllergenTest
    {
        [Fact]
        public void TestAllergenFreeIngredientsAppear()
        {
            FoodList foodlist = new FoodList("input/day21.txt");
            Assert.Equal(5, foodlist.AllergenFreeIngredientsAppear());
        }
        [Fact]
        public void TestCanonicalList()
        {
            FoodList foodlist = new FoodList("input/day21.txt");
            Assert.Equal("mxmxvkd,sqjhc,fvjkl", foodlist.CanonicalList());
        }
    }   
}