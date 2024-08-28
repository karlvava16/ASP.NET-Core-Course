using LoginUsingMiddleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<HttpMethodMiddleware>();
builder.Services.AddTransient<LoginMiddleware>();

var app = builder.Build();

app.UseHttpMethodMiddleware();
app.UseLoginMiddleware();

app.Run();
