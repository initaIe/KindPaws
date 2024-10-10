using FluentValidation.Results;
using KindPaws.API.Response;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Extensions;

public static class ResponseExtensions
{
    // Handler error result to error response
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responseError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    // Fluent validation result to error response
    public static ActionResult ToValidationErrorResponse(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            throw new InvalidOperationException("Result can not be succeed");

        var validationErrors = validationResult.Errors;

        var responseErrors = from validationError in validationErrors
            let error = Error.Deserialize(validationError.ErrorMessage)
            select new ResponseError(
                error.Code,
                error.Message,
                validationError.PropertyName);

        var envelope = Envelope.Error(responseErrors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}