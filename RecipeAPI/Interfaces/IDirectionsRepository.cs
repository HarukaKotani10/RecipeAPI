using RecipeAPI.Models;

namespace RecipeAPI.Interfaces
{
    public interface IDirectionsRepository
    {
        ICollection<Directions> GetDirections();
        Directions GetDirection(int id);
        bool HasDirections(int id);
        bool CreateDirection(Directions direction);
        bool UpdateDirection(Directions direction);
        bool DeleteDirection(Directions direction);
        bool Save();
    }
}
