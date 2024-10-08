using ControllersExample.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<HomeController>();
builder.Services.AddControllers(); //adds all the controller classes as services

var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();


app.MapControllers();

app.Run();
