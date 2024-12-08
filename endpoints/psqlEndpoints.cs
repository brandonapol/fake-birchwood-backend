using BirchwoodSheets;

namespace psqlEndpoints;

public static class Endpoints
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {

        app.MapGet("/api/blogs", async (YourDbContext dbContext) =>
            await dbContext.Blogs
                .Select(blog => new { blog.Id, blog.Title })
                .ToListAsync()
        );

        return app;
    }
}