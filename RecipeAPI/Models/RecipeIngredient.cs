namespace RecipeAPI.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipes Recipes { get; set; }
        public int IngredientId { get; set; }
        public Ingredients Ingredients { get; set; }
        public double Amount { get; set; }
    }
}
