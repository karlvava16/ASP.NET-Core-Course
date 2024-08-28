using System.Text.RegularExpressions;

namespace LoginUsingMiddleware.Middleware
{

    public class LoginMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            string resp = "";

            if (!context.Request.Query.ContainsKey("email"))
            {
                context.Response.StatusCode = 400;
                resp += "Invalid input for \'email\'\n";
            }
            if (!context.Request.Query.ContainsKey("password"))
            {
                context.Response.StatusCode = 400;
                resp += "Invalid input for \'password\'\n";
            }

            if (context.Response.StatusCode == 200)
            {
                if (IsLoginValid(context.Request.Query["email"].First(), context.Request.Query["password"].First()))
                {
                    resp = "Login Successful";
                   
                }
                else
                {
                    context.Response.StatusCode = 400;
                    resp = "Login Failed";
                }

            }

            await context.Response.WriteAsync(resp);
            await next(context);
        }

        public bool IsLoginValid(string? email, string? password)
        {
            if (email == null || password == null)
                return false;

            Regex regexEmail = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            Regex regexPassword = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");

            return regexEmail.IsMatch(email) && regexPassword.IsMatch(password);
        }

    }

    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
