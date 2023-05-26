namespace RecipeAPI.Models
{
    public class Recipes
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Nullable property
        public string? Description { get; set; } // Nullable property
        public string? ImageUrl { get; set; } // Nullable property
        public List<RecipeIngredient>? RecipeIngredients { get; set; }
        /*        public int Servings { get; set; }
                public int PrepTime { get; set; }
                public int CookTime { get; set; }
                public DateTime CreatedAt { get; set; }*/
        public ICollection<Directions> Directions { get; set; }

    }
}
