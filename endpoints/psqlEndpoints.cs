namespace psqlEndpoints;
using MainProgram;
using Microsoft.EntityFrameworkCore;
using BlogContext;
public static class Endpoints
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {


        app.MapGet("/api/blogs", async (BlogDbContext dbContext) =>
            await dbContext.Blogs
                .Select(blog => new { blog.Id, blog.Title })
                .ToListAsync()
        );

        return app;
    }
}