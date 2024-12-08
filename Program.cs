using BirchwoodSheets;
using Blog;
using Supabase;
using sheetsEndpoints;
using Npgsql;
using Aspire;
using Microsoft.EntityFrameworkCore;

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
            var config = GetGoogleSheetsAndSupabaseConfig(builder);
            var supabaseOptions = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            builder.Services.AddDbContext<YourDbContext>(options => options.UseNpgsql(config.PostgresString));
            builder.Services.AddSingleton(provider => new Client(config.SupabaseKey, config.SupabaseUrl, supabaseOptions));
            builder.Services.AddSingleton(new GoogleSheetsService(config.CredentialsPath, config.SpreadsheetId));
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

        private static (string PostgresString, string CredentialsPath, string SpreadsheetId, string SupabaseUrl, string SupabaseKey) GetGoogleSheetsAndSupabaseConfig(WebApplicationBuilder builder)
        {
            return (
                builder.Configuration["Postgres:ConnectionString"] ?? throw new InvalidOperationException("Postgres is wrong"),
                builder.Configuration["GoogleSheets:CredentialsPath"] ?? throw new InvalidOperationException("GoogleSheets:CredentialsPath not configured"),
                builder.Configuration["GoogleSheets:SpreadsheetId"] ?? throw new InvalidOperationException("GoogleSheets:SpreadsheetId not configured"),
                builder.Configuration["Supabase:Url"] ?? throw new InvalidOperationException("Supabase:Url not configured"),
                builder.Configuration["Supabase:Key"] ?? throw new InvalidOperationException("Supabase:Key not configured")
            );
        }
    }
}