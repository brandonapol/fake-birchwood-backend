using BirchwoodSheets;
using Blog;
using Supabase;
using sheetsEndpoints;

var builder = WebApplication.CreateBuilder(args);


var credentialsPath = builder.Configuration["GoogleSheets:CredentialsPath"]
    ?? throw new InvalidOperationException("GoogleSheets:CredentialsPath configuration is required");
var spreadsheetId = builder.Configuration["GoogleSheets:SpreadsheetId"]
    ?? throw new InvalidOperationException("GoogleSheets:SpreadsheetId configuration is required");
var supabaseUrl = builder.Configuration["Supabase:Url"] 
    ?? throw new InvalidOperationException("Supabase:Url configuration is required");
var supabaseKey = builder.Configuration["Supabase:Key"]
    ?? throw new InvalidOperationException("Supabase:Key configuration is required");

var supabaseOptions = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddSingleton(provider => new Client(supabaseUrl, supabaseKey, supabaseOptions));
builder.Services.AddSingleton(new GoogleSheetsService(credentialsPath, spreadsheetId));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapApplicationEndpoints();

app.Run();