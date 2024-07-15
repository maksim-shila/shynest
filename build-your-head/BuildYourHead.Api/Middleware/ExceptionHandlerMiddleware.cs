using System.Net;
using System.Text.Json;
using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Exceptions;

namespace BuildYourHead.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string message;
        switch (exception)
        {
            case NotFoundException:
            case EntityNotFoundException:
                statusCode = (int) HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case ValidationException:
                statusCode = (int) HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case AlreadyExistsException:
                statusCode = (int) HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            default:
                statusCode = (int) HttpStatusCode.InternalServerError;
                message = "Unknown Error";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse {Error = message};
        var responseJson = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(responseJson);
    }
}