
using ChatApp.Domain.Exceptions;
using System.Text.Json;

namespace ChatApp.API.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = GetStatusCode(ex);
        var respone = new
        {
            title = GetTitle(ex),
            status = statusCode,
            detail = ex.Message,
            errors = GetErrors(ex)
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(respone));
    }

    private static IReadOnlyCollection<ChatApp.Application.Exceptions.ValidationError> GetErrors(Exception ex)
    {
        IReadOnlyCollection<ChatApp.Application.Exceptions.ValidationError> errors = null;
        if (ex is ChatApp.Application.Exceptions.ValidationException validationException)
        {
            errors = validationException.Errors;
        }
        return errors;
    }

    private static string GetTitle(Exception ex) => ex switch
    {
        DomainException applicationException => applicationException.Title,
        _ => "Server error",
    };


    private static int GetStatusCode(Exception ex) => ex switch
    {
        BadRequestException => StatusCodes.Status400BadRequest,
        NotFoundException => StatusCodes.Status404NotFound,
        FormatException => StatusCodes.Status422UnprocessableEntity,
        FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError,
    };
}
