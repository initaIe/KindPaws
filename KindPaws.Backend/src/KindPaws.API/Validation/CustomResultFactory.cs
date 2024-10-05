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

        List<ResponseError> responseErrors = [];

        foreach (var (invalid, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrorsIteration = from errorMessage in validationErrors
                let error = Error.Deserialize(errorMessage)
                select new ResponseError(
                    error.Code,
                    error.Message,
                    invalid);

            responseErrors.AddRange(responseErrorsIteration);
        }

        var envelope = Envelope.Error(responseErrors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}