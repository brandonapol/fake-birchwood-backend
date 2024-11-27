using BirchwoodSheets;

namespace sheetsEndpoints;

public static class Endpoints
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/hello", () => new { message = "hello world" });

        app.MapGet("/sheets", async (GoogleSheetsService sheetsService) =>
        {
            var range = "Sheet1!A1:B1";
            var values = (await sheetsService.ReadValuesAsync(range))[0];
            return values;
        });

        return app;
    }
}