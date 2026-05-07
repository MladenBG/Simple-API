using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementAPI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        // Hardcoded token for testing purposes
        private const string ValidToken = "Bearer secret-token-123"; 

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the Authorization header is present
            if (!context.Request.Headers.TryGetValue("Authorization", out var extractedToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Missing Authorization header.");
                return;
            }

            // Validate the token
            if (extractedToken != ValidToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid token.");
                return;
            }

            // Token is valid, proceed to the next middleware
            await _next(context);
        }
    }
}