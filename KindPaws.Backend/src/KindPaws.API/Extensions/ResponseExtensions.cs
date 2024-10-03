using System.Runtime.InteropServices.JavaScript;
using KindPaws.API.Response;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Extensions;

public static class ResponseExtensions
{
    public static IActionResult ToResponse<T>(this Result<T, Error> result)
    {
        if (result.IsSuccess)
            return new ObjectResult(Envelope.Ok(result.Value));

        var statusCode = result.Error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var envelope = Envelope.Error(result.Error);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
}