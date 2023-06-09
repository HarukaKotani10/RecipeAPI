using RecipeAPI.Models;

namespace RecipeAPI.Interfaces
{
    public interface IRecipeIngredientRepository
    {
        ICollection<RecipeIngredient> GetRecipeIngredients();
        RecipeIngredient GetRecipeIngredient(int recipeId, int ingredientid);
        bool CreateRecipeIngredient(RecipeIngredient recipeIngredient);
        bool UpdateRecipeIngredient(RecipeIngredient recipeIngredient);
        bool DeleteRecipeIngredient(RecipeIngredient recipeIngredient);
        bool Save();
    }
}
