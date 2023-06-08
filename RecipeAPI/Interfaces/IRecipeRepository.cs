using RecipeAPI.Models;

namespace RecipeAPI.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipes> GetRecipes();
        Recipes GetRecipe(int id);
        bool HasRecipe(int id);
        bool CreateRecipe(Recipes recipe);
        bool UpdateRecipe(Recipes recipe);
        bool DeleteRecipe(Recipes recipe);
        bool Save();
    }
}
