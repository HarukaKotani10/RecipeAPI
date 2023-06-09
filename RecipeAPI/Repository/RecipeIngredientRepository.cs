using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

namespace RecipeAPI.Repository
{
    public class RecipeIngredientRepository:IRecipeIngredientRepository
    {
        private DataContext _context;

        public RecipeIngredientRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            throw new NotImplementedException();
        }

        public RecipeIngredient GetRecipeIngredient(int recipeId, int ingredientid)
        {
            return _context.RecipeIngredients.Where(r => r.RecipeId == recipeId && r.IngredientId == ingredientid).FirstOrDefault();
        }

        public ICollection<RecipeIngredient> GetRecipeIngredients()
        {
            return _context.RecipeIngredients.OrderBy(r => r.RecipeId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            throw new NotImplementedException();
        }
    }
}
