using RecipeAPI.Models;

namespace RecipeAPI.Dto
{
    public class RecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string? Amount { get; set; }
    }
}
