using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace eCommerceApp.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {   
                await _next(context);
            }
            catch (DbUpdateException ex)
            {
                var logger = context.RequestServices.GetRequiredService<IApplogger<ExceptionHandlingMiddleware>>();
                MySqlException? innerException = ex.InnerException as MySqlException;
                context.Response.ContentType = "application/json";
                if (innerException != null)
                {
                    logger.LogError(innerException, "MySqlException exception");
                    switch (innerException.Number)
                    {
                        case 1062: // Duplicate entry
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Duplicate value violates unique constraint.");
                            break;

                        case 1048: // Column cannot be null
                        case 1364: // Field doesn't have default value
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("Required field is missing.");
                            break;

                        case 1452: // FK insert/update fail
                        case 1451: // FK delete restrict
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Foreign key constraint violation");
                            break;
                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("Database error occurred.");
                            break;
                    }
                }
                else
                {
                    logger.LogError(ex, "Related EFCore exception");

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("An error occurred while saving the entity changes");
                }
                
            }
            catch (Exception ex)
            {
                var logger = context.RequestServices.GetRequiredService<IApplogger<ExceptionHandlingMiddleware>>();
                logger.LogError(ex, "Unnknown exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred:" + ex.Message);
            }
        }
    }
}
