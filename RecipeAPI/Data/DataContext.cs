using Microsoft.EntityFrameworkCore;
using RecipeAPI.Models;

namespace RecipeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Directions> Directions { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(r => r.Recipes)
                .WithMany(ri => ri.RecipeIngredients)
                .HasForeignKey(r => r.RecipeId);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(r => r.Ingredients)
                .WithMany(ri => ri.RecipeIngredients)
                .HasForeignKey(i => i.IngredientId);
        }
    }
}
