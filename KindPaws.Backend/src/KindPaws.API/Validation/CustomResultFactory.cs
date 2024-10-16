using KindPaws.API.Response;
using KindPaws.Domain.Shared.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace KindPaws.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails == null)
            throw new InvalidOperationException($"{nameof(ValidationProblemDetails)} can not be null");

        List<Error> errors = [];

        foreach (var (invalidPropertyName, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrorsIteration = from errorMessage in validationErrors
                let error = Error.Deserialize(errorMessage)
                select Error.Validation(
                    error.Code,
                    error.Message,
                    invalidPropertyName);

            errors.AddRange(responseErrorsIteration);
        }

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}