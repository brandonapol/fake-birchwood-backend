using Npgsql;
using RecipeContext;
using Recipe;

namespace api;

class Program
{
    public static async Task<IEnumerable<RecipeType>> GetRecipes()
    {
        string connectionString = "Host=localhost;Port=5432;Username=brandonapol;Database=mydatabase";
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        string query = "SELECT * FROM recipes;";

        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        var recipes = new List<RecipeType>();
        while (await reader.ReadAsync())
        {
            recipes.Add(new RecipeType
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                // Ingredients = new List<RecipeIngredient>()
            });
        }

        return recipes;
    }
}
