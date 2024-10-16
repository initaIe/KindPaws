using KindPaws.API.Response;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = GetStatusCodeForErrorType(error.Type);

        var envelope = Envelope.Error(error.ToErrorList());

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToResponse(this ErrorList errors)
    {
        if (!errors.Any())
            return new ObjectResult(Envelope.Error(errors))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        var distinctErrorTypes = errors
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorTypes.First());

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    // Fluent validation result to error response
    // public static ActionResult ToValidationErrorResponse(this ValidationResult validationResult)
    // {
    //     if (validationResult.IsValid)
    //         throw new InvalidOperationException("Result can not be succeed");
    //
    //     var validationErrors = validationResult.Errors;
    //
    //     var responseErrors = from validationError in validationErrors
    //         let error = Error.Deserialize(validationError.ErrorMessage)
    //         select new ResponseError(
    //             error.Code,
    //             error.Message,
    //             validationError.PropertyName);
    //
    //     var envelope = Envelope.Error(responseErrors);
    //
    //     return new ObjectResult(envelope)
    //     {
    //         StatusCode = StatusCodes.Status400BadRequest
    //     };
    // }
}