using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

namespace RecipeAPI.Repository
{
    public class DirectionRepository : IDirectionRepository
    {
        private DataContext _context;

        public DirectionRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateDirection(Directions direction)
        {
            _context.Add(direction);
            return Save();
        }

        public bool DeleteDirection(Directions direction)
        {
            _context.Remove(direction);
            return Save();
        }

        public ICollection<Directions> GetDirections()
        {
            return _context.Directions.OrderBy(d => d.Id).ToList();
        }

        public Directions GetDirection(int id)
        {
            return _context.Directions.Where(d => d.Id == id).FirstOrDefault();
        }

        public bool HasDirections(int id)
        {
            return _context.Directions.Any(d => d.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDirection(Directions direction)
        {
            _context.Update(direction);
            return Save();
        }
    }
}
