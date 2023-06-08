using RecipeAPI.Models;

namespace RecipeAPI.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<RecipeIngredient>? RecipeIngredients { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
