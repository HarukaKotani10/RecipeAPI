namespace RecipeAPI.Models
{
    public class Directions
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? Instruction { get; set; }
        public int RecipeId { get; set; }
        public Recipes? Recipes { get; set; }
    }
}
