using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

namespace RecipeAPI.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private DataContext _context;

        public IngredientRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateIngredient(Ingredients ingredient)
        {
            _context.Add(ingredient);
            return Save();
        }

        public bool DeleteIngredientn(Ingredients ingredient)
        {
            _context.Remove(ingredient);
            return Save();
        }

        public Ingredients GetIngredient(int id)
        {
            return _context.Ingredients.Where(i => i.Id == id).FirstOrDefault();
        }

        public ICollection<Ingredients> GetIngredientByRecipe(int recipeId)
        {
            return _context.RecipeIngredients
                .Where(ri => ri.RecipeId == recipeId)
                .Select(ri => ri.Ingredient)
                .ToList();
        }

        public ICollection<Ingredients> GetIngredients()
        {
            return _context.Ingredients.OrderBy(i => i.Id).ToList();
        }

        public bool HasIngredient(int id)
        {
            return _context.Ingredients.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saved =  _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UpdateIngredient(Ingredients ingredient)
        {
            _context.Ingredients.Update(ingredient);
            return Save();
        }
    }
}
