using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LoginUsingMiddleware.Middleware
{
    public class HttpMethodMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method == "POST" && context.Request.Path == "/")
            {
                context.Response.StatusCode = 200;
                await next(context);

            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("No response");
            }
        }
    }

    public static class HttpMethodMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMethodMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMethodMiddleware>();
        }
    }
}
