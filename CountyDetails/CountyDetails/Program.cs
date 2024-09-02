using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Dictionary<int, string> countries = new Dictionary<int, string>();
countries.Add(1, "United States");
countries.Add(2, "Canada");
countries.Add(3, "United Kingdom");
countries.Add(4, "India");
countries.Add(5, "Japan");


app.MapGet("/countries", async context =>
{
    foreach (var country in countries)
    {
        await context.Response.WriteAsync(country.Key + ", " + country.Value + "\n");
    }
});


app.MapGet("/countries/{countryID:int}", async context =>
{
    int countryId = Convert.ToInt32(context.Request.RouteValues["countryID"]);


    if (countryId >= 1 && countryId <= 100)
    {
        if (countries.ContainsKey(countryId))
        {
            await context.Response.WriteAsync(countries[countryId]);
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");
        }
    }

    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("The CountryID should be between 1 and 100");
    }
});

app.Run();
