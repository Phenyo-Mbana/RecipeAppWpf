using Microsoft.VisualBasic;
using RecipeAppWpf.Models; // Updated namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecipeAppWpf
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CaptureRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Interaction.InputBox("Enter recipe name:", "Recipe Name");
            if (!string.IsNullOrEmpty(recipeName))
            {
                Recipe newRecipe = new Recipe(recipeName);

                int numIngredients = int.Parse(Interaction.InputBox("Enter number of ingredients:", "Number of Ingredients"));
                for (int i = 0; i < numIngredients; i++)
                {
                    string ingredientName = Interaction.InputBox($"Enter name of ingredient {i + 1}:", "Ingredient Name");
                    double ingredientQuantity = double.Parse(Interaction.InputBox($"Enter quantity of {ingredientName}:", "Quantity"));
                    string ingredientUnit = Interaction.InputBox($"Enter unit of measurement for {ingredientName}:", "Unit");
                    double ingredientCalories = double.Parse(Interaction.InputBox($"Enter calories for {ingredientName}:", "Calories"));
                    string ingredientFoodGroup = Interaction.InputBox($"Enter food group for {ingredientName}:", "Food Group");

                    newRecipe.AddIngredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup);
                }

                int numSteps = int.Parse(Interaction.InputBox("Enter number of steps:", "Number of Steps"));
                for (int i = 0; i < numSteps; i++)
                {
                    string stepDescription = Interaction.InputBox($"Enter description for step {i + 1}:", "Step Description");
                    newRecipe.AddStep(stepDescription);
                }

                recipes.Add(newRecipe);
                MessageBox.Show("Recipe captured successfully!");
            }
        }

        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder recipeDetails = new StringBuilder();
            foreach (Recipe recipe in recipes)
            {
                recipeDetails.AppendLine($"Recipe Name: {recipe.Name}");
                recipeDetails.AppendLine("Ingredients:");
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    recipeDetails.AppendLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories)");
                }
                recipeDetails.AppendLine("Steps:");
                foreach (Step step in recipe.Steps)
                {
                    recipeDetails.AppendLine($"- {step.Description}");
                }
                recipeDetails.AppendLine();
            }
            DynamicDisplay.Content = new TextBlock
            {
                Text = recipeDetails.ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16,
                Margin = new Thickness(10),
                Foreground = Brushes.Black
            };
        }

        private void ChooseRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Interaction.InputBox("Enter the name of the recipe to display:", "Choose Recipe");
            Recipe chosenRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (chosenRecipe != null)
            {
                chosenRecipe.DisplayRecipe();
                DynamicDisplay.Content = new TextBlock
                {
                    Text = $"Recipe: {chosenRecipe.Name}\n\n" +
                           $"Ingredients:\n{string.Join("\n", chosenRecipe.Ingredients.Select(i => $"- {i.Quantity} {i.Unit} of {i.Name} ({i.Calories} calories)"))}\n\n" +
                           $"Steps:\n{string.Join("\n", chosenRecipe.Steps.Select((s, index) => $"{index + 1}. {s.Description}"))}\n"
                };
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Interaction.InputBox("Enter the name of the recipe to reset quantities:", "Reset Recipe");
            Recipe recipeToReset = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipeToReset != null)
            {
                recipeToReset.ResetQuantitiesToOriginal();
                MessageBox.Show("Recipe quantities reset to original.");
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            MessageBox.Show("All recipes cleared.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
