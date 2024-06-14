namespace RecipeAppWpf.Models
{
    public class Ingredient
    {
        public string Name { get; set; } = string.Empty; // Initialize with default value
        public double Quantity { get; set; }
        public string Unit { get; set; } = string.Empty; // Initialize with default value
        public double Calories { get; set; }
        public string FoodGroup { get; set; } = string.Empty; // Initialize with default value
    }
}
