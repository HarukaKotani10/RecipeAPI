using RecipeAPI.Models;

namespace RecipeAPI.Interfaces
{
    public interface IIngredientRepository
    {
        ICollection<Ingredients> GetIngredients();
        Ingredients GetIngredient(int id);
        ICollection<Ingredients> GetIngredientByRecipe(int recipeId);
        bool HasIngredient(int id);
        bool CreateIngredient(Ingredients ingredient);
        bool UpdateIngredient(Ingredients ingredient);
        bool DeleteIngredientn(Ingredients ingredient);
        bool Save();
    }
}
