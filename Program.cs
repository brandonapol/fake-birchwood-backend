using Microsoft.EntityFrameworkCore;
using psqlEndpoints;
using RecipeContext;

namespace MainProgram
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();
            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var connString = GetDbConfig();

            builder.Services.AddDbContext<RecipeDbContext>(options => options.UseNpgsql(connString));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        private static void ConfigureApp(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapApplicationEndpoints();
        }

        private static string GetDbConfig()
        {
            var connString = "Host=localhost;Port=5432;Username=brandonapol;Database=mydatabase";
            
            return connString;
        }
    }
}