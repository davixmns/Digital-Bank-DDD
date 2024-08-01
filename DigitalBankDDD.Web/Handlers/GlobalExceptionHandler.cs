using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace DigitalBankDDD.Web.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is DomainException or ApplicationException)
        {
            Console.WriteLine(exception.ToString());
            var failureResult = ApiResult<string>.Failure(exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(failureResult, cancellationToken);
        }
        else
        {
            Console.WriteLine("Internal server error");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            _logger.LogError(exception, "An internal server error occurred: {Message}", exception.Message);
            var apiResult = ApiResult<string>.Failure("An unexpected error occurred. Please try again later.");
            await httpContext.Response.WriteAsJsonAsync(apiResult, cancellationToken);
        }

        return true;
    }
}