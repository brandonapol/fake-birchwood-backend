namespace psqlEndpoints;
using MainProgram;
using Microsoft.EntityFrameworkCore;
using RecipeContext;
using api;
public static class Endpoints
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {

        app.MapGet("/api/recipes", async (RecipeDbContext dbContext) =>
            await Program.GetRecipes());

        return app;
    }
}