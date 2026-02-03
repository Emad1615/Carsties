
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuctionService.Middleware
{
    public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment environment) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError(ex, ex.Message, ex.InnerException);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = environment.IsDevelopment()
                ? new RequestHelpers.AppException(context.Response.StatusCode, ex.Message, ex.StackTrace ?? string.Empty)
                : new RequestHelpers.AppException(context.Response.StatusCode, "Internal Server Error", string.Empty);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
        public static async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            var ValidationResult = new Dictionary<string, string[]>();
            if (ex.Errors is not null)
            {
                foreach (var error in ex.Errors)
                {
                    if (ValidationResult.TryGetValue(error.PropertyName, out var existingError))
                    {
                        ValidationResult[error.PropertyName] = [.. existingError, error.ErrorMessage];
                    }
                    else
                    {
                        ValidationResult[error.PropertyName] = [error.ErrorMessage];
                    }
                }
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationProbelmDetails = new ValidationProblemDetails(ValidationResult)
                {
                    Status = context.Response.StatusCode,
                    Type = "ValidationFailure",
                    Title = "One or more validation errors occurred.",
                    Detail = "See the errors property for details."
                };
                await context.Response.WriteAsJsonAsync(validationProbelmDetails);
            }
        }
    }
}
