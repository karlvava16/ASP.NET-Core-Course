var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("userName"),
    app => { 
        app.Use(async (context, next)  => {
            await context.Response.WriteAsync("Hello from Middleware branch!\n");
            await next(context);
        });
    
    });

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello World!\n");
});

app.Run();
