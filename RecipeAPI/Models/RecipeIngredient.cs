namespace RecipeAPI.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipes? Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredients? Ingredient { get; set; }
        public string? Amount { get; set; }
    }
}
