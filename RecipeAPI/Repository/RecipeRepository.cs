using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

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
            throw new NotImplementedException();
        }

        public bool DeleteRecipe(Recipes recipe)
        {
            throw new NotImplementedException();
        }

        public Recipes GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();   
        }

        public ICollection<Recipes> GetRecipes()
        {
            return _context.Recipes.OrderBy(r => r.Id).ToList();
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
