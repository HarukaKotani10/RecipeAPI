using RecipeAPI.Models;

namespace RecipeAPI.Dto
{
    public class DirectionDto
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? Instruction { get; set; }
        public int RecipeId { get; set; }
    }
}
