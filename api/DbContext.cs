using Recipe;
using Microsoft.EntityFrameworkCore;

namespace RecipeContext;

public class RecipeDbContext : DbContext
{
    public DbSet<RecipeType> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options)
    { }
}