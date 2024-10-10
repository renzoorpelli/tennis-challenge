using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Exceptions;

namespace TennisChallenge.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware
    : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(e, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, response) = GetResponseForException(exception);
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    private (int StatusCode, ApiResponse<object> Response) GetResponseForException(Exception exception)
    {
        return exception switch
        {
            DomainException ex => (
                StatusCodes.Status400BadRequest,
                CreateBadRequestResponse("domain_exception", ex.Message)
            ),
            ValidationException ex => (
                StatusCodes.Status400BadRequest,
                CreateValidationErrorResponse(ex.Errors)
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                CreateInternalErrorResponse()
            )
        };
    }

    private ApiResponse<object> CreateBadRequestResponse(string errorType, string message)
    {
        var error = new Error(errorType, message);
        return ApiResponseHelpers.SetBadRequestResponse<object>(new List<string> { message });
    }

    private ApiResponse<object> CreateValidationErrorResponse(IEnumerable<ValidationFailure> errors)
    {
        var validationErrors = errors.Select(e => e.ErrorMessage).ToList();
        return ApiResponseHelpers.SetBadRequestResponse<object>(validationErrors);
    }

    private ApiResponse<object> CreateInternalErrorResponse()
    {
        return ApiResponseHelpers.SetInternalErrorResponse<object>("An error has occurred on the server");
    }

    private record Error(string Code, string? Message);

    private record ValidationError(string Code, List<string> Errors) : Error(Code, "validation failed")
    {
    }
}