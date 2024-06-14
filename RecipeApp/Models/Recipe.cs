using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeAppWpf.Models
{
    public class Recipe
    {
        private List<Ingredient> originalIngredients;
        public event Action<string>? RecipeExceedsCalories;

        public string Name { get; }
        public List<Ingredient> Ingredients { get; }
        public List<Step> Steps { get; }

        public Recipe(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
            originalIngredients = new List<Ingredient>();
        }

        public void AddIngredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });
            originalIngredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });
        }

        public void AddStep(string description)
        {
            Steps.Add(new Step { Description = description });
        }

        public void ResetQuantitiesToOriginal()
        {
            Ingredients.Clear();
            Ingredients.AddRange(originalIngredients);
        }

        public void ClearRecipeData()
        {
            Ingredients.Clear();
            Steps.Clear();
            originalIngredients.Clear();
        }

        public void DisplayRecipe()
        {
            double totalCalories = Ingredients.Sum(i => i.Calories);

            if (totalCalories > 300)
                RecipeExceedsCalories?.Invoke(Name);
        }
    }
}
