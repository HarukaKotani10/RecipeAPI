using Microsoft.EntityFrameworkCore;
using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;
using System.Collections;

namespace RecipeAPI.Repository
{
    public class RecipeRepository: IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CreateRecipe(Recipes recipe)
        {
            _context.Add(recipe);
            return Save();
        }

        public bool DeleteRecipe(Recipes recipe)
        {
            _context.Remove(recipe);
            return Save();
        }

        public Recipes GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();   
        }

        public ICollection<Recipes> GetRecipes()
        {
            return _context.Recipes.OrderBy(r => r.Id).ToList();
        }
        public ICollection<Recipes> GetRecipesByIngredients(int[] ingredients)
        {

            var filteredRecipes = _context.Recipes.AsEnumerable().Where(recipe =>
                recipe.RecipeIngredients != null && ingredients.All(ingredientId =>
                    recipe.RecipeIngredients.Any(ri => ri.IngredientId == ingredientId))
            ).ToList();

            return filteredRecipes;
        }

        public bool HasRecipe(int id)
        {
            return _context.Recipes.Any(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;    
        }

        public bool UpdateRecipe(Recipes recipe)
        {
            _context.Update(recipe);
            return Save();
        }
    }
}
