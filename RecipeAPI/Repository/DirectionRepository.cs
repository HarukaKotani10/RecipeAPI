using RecipeAPI.Data;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

namespace RecipeAPI.Repository
{
    public class DirectionRepository : IDirectionsRepository
    {
        private DataContext _context;
        public bool CreateDirection(Directions direction)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDirection(Directions direction)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
