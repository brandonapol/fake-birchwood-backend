using Blog;
using Microsoft.EntityFrameworkCore;

namespace BlogContext;

public class BlogDbContext : DbContext
{
    public DbSet<BlogType> Blogs { get; set; }
    public DbSet<BlogIngredient> BlogIngredients { get; set; }
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    { }
}