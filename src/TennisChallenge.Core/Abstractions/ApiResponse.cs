using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TennisChallenge.Core.Abstractions;

public record ApiResponse<TPayload>(int Code, TPayload? Payload, ProblemDetails? ProblemDetails = null, string? Message = null)
    where TPayload : class
{
    public bool IsSuccess => Code is >= 200 and < 300;
}

public static class ApiResponseHelpers
{
    public static ApiResponse<TPayload> SetSuccessResponse<TPayload>(TPayload payload, string? message = null)
        where TPayload : class
        => new ApiResponse<TPayload>(StatusCodes.Status200OK, payload, null, message ?? "Request succeeded");

    public static ApiResponse<TPayload> SetNotFoundResponse<TPayload>(string? detail = null)
        where TPayload : class
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = detail ?? "The requested resource could not be found."
        };
        return new ApiResponse<TPayload>(StatusCodes.Status404NotFound, null, problemDetails);
    }

    public static ApiResponse<TPayload> SetBadRequestResponse<TPayload>(List<string> errors, string? detail = null)
        where TPayload : class
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Invalid request",
            Status = StatusCodes.Status400BadRequest,
            Detail = detail ?? "The request contains invalid data.",
            Extensions = { { "errors", errors } }
        };
        return new ApiResponse<TPayload>(StatusCodes.Status400BadRequest, null, problemDetails);
    }

    public static ApiResponse<TPayload> SetInternalErrorResponse<TPayload>(string? detail = null)
        where TPayload : class
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Internal server error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = detail ?? "An unexpected error occurred on the server."
        };
        return new ApiResponse<TPayload>(StatusCodes.Status500InternalServerError, null, problemDetails);
    }
}

public static class MapApiResponse
{
    public static IActionResult ToActionResult<TPayload>(ApiResponse<TPayload>? apiResponse = null)
        where TPayload : class
    {
        if (apiResponse is null)
        {
            return new OkObjectResult(null);
        }
        return apiResponse.Code switch
        {
            StatusCodes.Status200OK => new OkObjectResult(apiResponse),
            StatusCodes.Status400BadRequest => new BadRequestObjectResult(apiResponse),
            StatusCodes.Status404NotFound => new NotFoundObjectResult(apiResponse),
            StatusCodes.Status500InternalServerError => new ObjectResult(apiResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            },
            _ => new ObjectResult(apiResponse.ProblemDetails)
            {
                StatusCode = apiResponse.Code
            }
        };
    }
}