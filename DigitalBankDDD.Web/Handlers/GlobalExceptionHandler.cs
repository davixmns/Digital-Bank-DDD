using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Exceptions;

namespace DigitalBankDDD.Web.Handlers;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
        catch (Exception ex)
        {
            if (ex is DomainException or ApplicationException)
                await HandleNonCriticalExceptionAsync(context, ex);
            else
                await HandleCriticalExceptionAsync(context, ex);
        }
    }

    private Task HandleNonCriticalExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var apiResult = AppResult<string>.Failure(exception.Message);
        return context.Response.WriteAsJsonAsync(apiResult);
    }

    private Task HandleCriticalExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred.");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var apiResult = AppResult<string>.Failure("An unexpected error occurred. Please try again later.");
        return context.Response.WriteAsJsonAsync(apiResult);
    }
}