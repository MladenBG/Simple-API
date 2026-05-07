using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserManagementAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container to support controllers.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the Middleware Pipeline in the strict order requested:

// 1. Error-handling middleware first
app.UseMiddleware<ErrorHandlingMiddleware>();

// 2. Authentication middleware next
app.UseMiddleware<AuthenticationMiddleware>();

// 3. Logging middleware last
app.UseMiddleware<LoggingMiddleware>();

// Map controller routes so the application knows how to handle HTTP requests.
app.MapControllers();

app.Run();