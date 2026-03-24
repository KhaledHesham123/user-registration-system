
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using User_Registration_System.Shared.Respones;

namespace User_Registration_System.Shared.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public ILogger<GlobalExceptionHandler> _logger { get; }
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler>logger )
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

            }
            catch (ValidationException ex)
            {

                var errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var messageString = string.Join(" | ", errorMessages);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new { message = messageString };
                await context.Response.WriteAsJsonAsync(response);
            }
           

            catch (Exception ex)
            {
                _logger.LogError(ex, "System Error");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { message = "Something went wrong on the server." };
                await context.Response.WriteAsJsonAsync(response);
            }

        }

      
    }
}
