using RoutingExample.CustomConstrains;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => {
    option.ConstraintMap.Add("months", typeof(MonthCustomConstraints));
});

var app = builder.Build();

app.UseRouting();


/*       |PREVIOUS VARIAND OF CREATING ENDPOINTS| 
          NOW YOU CAN DO IT RIGHT IN Program.cs           */

//app.UseEndpoints(endpoints => {
//    endpoints.Map("files/{filename}.{extensions}", async context =>
//    {
//        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
//        string? extensions = Convert.ToString(context.Request.RouteValues["extensions"]);
//        await context.Response.WriteAsync($"In files - {fileName} - {extensions}");
//    });

//    endpoints.Map("employee/profile/{employeeName=vlad}", async context =>
//    {
//        string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
//        await context.Response.WriteAsync($"Employee - {employeeName}");
//    });


//    endpoints.Map("product/details/{id?}", async context => {
//        if (context.Request.RouteValues.ContainsKey("id"))
//        {
//            int id = Convert.ToInt32(context.Request.RouteValues["id"]);
//            await context.Response.WriteAsync($"products details - id: {id}");
//        }
//        else
//        {

//        }
//    });
//});

app.Map("files/{filename}.{extensions}", async context =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extensions = Convert.ToString(context.Request.RouteValues["extensions"]);
    await context.Response.WriteAsync($"In files - {fileName} - {extensions}");
});

app.Map("employee/profile/{employeeName:length(3,7):alpha=vlad}", async context =>
{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
    await context.Response.WriteAsync($"Employee - {employeeName}");
});

app.Map("product/details/{id:int}", async context =>
{
    if (context.Request.RouteValues.ContainsKey("id"))
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"products details - id: {id}");
    }
    else
    {
        await context.Response.WriteAsync($"products details - id: is not supplied");
    }
});

app.Map("daily-digest-report/{reportdate:datetime}", async context =>
{
    var reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
    await context.Response.WriteAsync($"In daily-digest-report - {reportDate.ToShortDateString()}");
});

app.Map("cities/{cityid:guid}", async context =>
{
    Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
    await context.Response.WriteAsync($"City info - {cityId}");

});

app.Map("sales-report/{year:int:min(1990)}/{month:months}", async context =>
{
    int year = Convert.ToInt32(context.Request.RouteValues["year"]);
    string? month = Convert.ToString(context.Request.RouteValues["month"]);

    Regex regex = new Regex(@"^(apr|jul|oct|jan)$");
    if (regex.IsMatch(month!))
    {
        await context.Response.WriteAsync($"sales report - {year} - {month}");
    }
    else
    {
        await context.Response.WriteAsync($"{month} is not allowed for sales report");

    }
});

app.Map("sales-report/2024/jan", async context =>
{
    await context.Response.WriteAsync($"sales report exclusively - 2024 - jan");

});


app.Map("/", async context =>
{
    await context.Response.WriteAsync($"Hello World from {context.Request.Path}");
});

// Fallback route for non-existent routes
app.MapFallback(async context =>
{
    await context.Response.WriteAsync($"No route matched at - {context.Request.Path}");

    // Alternatively, you can redirect to the root:
    //context.Response.Redirect("/");
});


app.Run();
