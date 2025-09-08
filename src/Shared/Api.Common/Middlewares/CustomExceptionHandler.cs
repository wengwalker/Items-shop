using Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Common.Middlewares;

public class CustomExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

    private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

        httpContext.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = exception is ValidationException validationException
                ? global::Api.Common.Middlewares.CustomExceptionHandler.GetValidationProblemDetails(httpContext, validationException)
                : global::Api.Common.Middlewares.CustomExceptionHandler.GetProblemDetails(httpContext, exception)
        });
    }

    private static ProblemDetails GetProblemDetails(HttpContext httpContext, Exception exception)
    {
        return new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "An unexpected error occured",
            Type = exception.GetType().Name,
            Detail = exception.Message
        };
    }

    private static ValidationProblemDetails GetValidationProblemDetails(HttpContext httpContext, ValidationException exception)
    {
        return new ValidationProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "One or more validation errors occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message,
            Errors = exception.Errors.ToDictionary(e => e.PropertyName, e => new string[] { e.ErrorMessage })
        };
    }
}
