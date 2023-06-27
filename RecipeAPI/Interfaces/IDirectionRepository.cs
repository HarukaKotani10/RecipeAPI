using RecipeAPI.Models;

namespace RecipeAPI.Interfaces
{
    public interface IDirectionRepository
    {
        ICollection<Directions> GetDirections();
        Directions GetDirection(int id);
        ICollection<Directions> GetDirectionsByRecipe(int recipeId);
        bool HasDirections(int id);
        bool CreateDirection(Directions direction);
        bool UpdateDirection(Directions direction);
        bool DeleteDirection(Directions direction);
        bool Save();
    }
}
