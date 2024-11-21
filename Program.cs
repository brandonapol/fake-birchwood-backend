using BirchwoodSheets;

var builder = WebApplication.CreateBuilder(args);


var credentialsPath = builder.Configuration["GoogleSheets:CredentialsPath"]
    ?? throw new InvalidOperationException("GoogleSheets:CredentialsPath configuration is required");
var spreadsheetId = builder.Configuration["GoogleSheets:SpreadsheetId"]
    ?? throw new InvalidOperationException("GoogleSheets:SpreadsheetId configuration is required");

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

app.MapGet("/hello", () => new { message = "hello world" });


app.MapGet("/sheets", async (GoogleSheetsService sheetsService) =>
{
    var range = "Sheet1!A1:B1";
    var values = (await sheetsService.ReadValuesAsync(range))[0];
    return values;
});

app.Run();