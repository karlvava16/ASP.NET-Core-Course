using Microsoft.Extensions.Primitives;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run(static async (HttpContext context) =>
{
    //context.Response.Headers["Content-Type"] = "text/html"; // how to interpretants this


    //TASK 1
    //string path = context.Request.Path;

    //await context.Response.WriteAsync("<h1>Hello</h1>");
    //await context.Response.WriteAsync("<h2>World</h2>");
    //await context.Response.WriteAsync($"<p>{path}</p>");

    //TASK 2
    //if(context.Request.Method == "GET")
    //{
    //    if(context.Request.Query.ContainsKey("id"))
    //    {
    //        string? id = context.Request.Query["id"];
    //        await context.Response.WriteAsync($"<p>id: {id}</p>");
    //    }
    //}

    //TASK 3
    //if (context.Request.Headers.ContainsKey("User-Agent"))
    //{
    //    string? userAgent = context.Request.Headers["User-Agent"];
    //    await context.Response.WriteAsync($"<p>{userAgent}</p>");
    //}

    //TASK 4
    //if (context.Request.Headers.ContainsKey("User-Agent"))
    //{
    //    string? auth = context.Request.Headers["AuthorizationKey"];
    //    await context.Response.WriteAsync($"<p>{auth}</p>");
    //}

    //TASK5
    //StreamReader reader = new StreamReader(context.Request.Body);
    //string body = await reader.ReadToEndAsync();

    //TASK6
    StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    Dictionary<string, StringValues> dict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if(dict.ContainsKey("firstName"))
    {
        string? firstName = dict["firstName"][0];
        await context.Response.WriteAsync($"<p>{firstName}</p>");
    }
});

app.Run();
