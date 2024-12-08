public class YourDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    
    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    { }
}