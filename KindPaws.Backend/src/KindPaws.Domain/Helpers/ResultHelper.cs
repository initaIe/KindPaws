using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Helpers;

public static class ResultHelper
{
    public static void AddErrorIfFailure<T>(this Result<T, string> result, List<string> errors)
    {
        if (result.IsFailure)
            errors.Add(result.Error);
    }

    public static void AddErrorsIfFailure<T>(this Result<T, List<string>> result, List<string> errors)
    {
        if (result.IsFailure)
            errors.AddRange(result.Error);
    }
}