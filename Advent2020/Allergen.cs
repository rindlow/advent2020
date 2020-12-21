using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Advent2020.Allergen
{
    public class Food
    {
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }
    }
    public class FoodList
    {
        List<Food> Foods;
        public FoodList(string filename)
        {
            Foods = new List<Food>();
            ReadFile(filename);
        }
        void ReadFile(string filename)
        {
            Regex lineRe = new Regex(@"^(.*) \(contains (.*)\)$");
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                Match match = lineRe.Match(line);
                if (!match.Success)
                {
                    throw new Exception($"Unparsable line '{line}')");
                }
                Food food = new Food();
                food.Ingredients = new List<string>(match.Groups[1].Value.Split(' '));
                food.Allergens = new List<string>(match.Groups[2].Value.Split(", "));
                Foods.Add(food);
            }
        }
        Dictionary<string, List<string>> Allergens()
        {
            Dictionary<string, List<string>> allergens = new Dictionary<string, List<string>>();
            foreach (Food food in Foods)
            {
                foreach (string allergen in food.Allergens)
                {
                    if (!allergens.ContainsKey(allergen))
                    {
                        allergens[allergen] = new List<string>(food.Ingredients);
                    }
                    else
                    {
                        allergens[allergen] = allergens[allergen].Intersect(food.Ingredients).ToList();
                    }
                }
            }
            while (allergens.Values.Any(set => set.Count > 1))
            {
                foreach (KeyValuePair<string, List<string>> kvp in allergens)
                {
                    if (kvp.Value.Count == 1)
                    {
                        foreach (KeyValuePair<string, List<string>> kvp2 in allergens)
                        {
                            if (kvp2.Key != kvp.Key)
                            {
                                kvp2.Value.Remove(kvp.Value.ToList()[0]);
                            }
                        }
                    }
                }
            }
            return allergens;
        }
        public int AllergenFreeIngredientsAppear()
        {
            Dictionary<string, List<string>> allergens = Allergens();
            List<string> allergenIngredients = allergens.Values.Aggregate(new List<string>(), (a, b) => a.Union(b).ToList(), a => a);
            List<string> allIngredients = Foods.Select(f => f.Ingredients).Aggregate(new List<string>(), (a, b) => a.Union(b).ToList(), a => a);
            List<string> allergenFreeIngredients = allIngredients.Except(allergenIngredients).ToList();
            int appear = 0;
            foreach (string allergenFree in allergenFreeIngredients)
            {
                foreach (Food food in Foods)
                {
                    if (food.Ingredients.Contains(allergenFree))
                    {
                        appear++;
                    }
                }
            }
            return appear;
        }
        public string CanonicalList()
        {
            Dictionary<string, List<string>> allergens = Allergens();
            List<string> ingredients = allergens.OrderBy(a => a.Key).Select(a => a.Value.First()).ToList();
            return string.Join(",", ingredients);
        }
    }
}