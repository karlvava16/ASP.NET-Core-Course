using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { WebRootPath = "myroot" });

var app = builder.Build();

app.UseStaticFiles(); // works with the web root path (myroot)
app.UseStaticFiles(options: new StaticFileOptions() 
{ FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))}); // works with the web root path (mywebroot)


app.UseRouting();

app.Map("/", async context =>
{
    await context.Response.WriteAsync("Hello");

});
app.Run();
