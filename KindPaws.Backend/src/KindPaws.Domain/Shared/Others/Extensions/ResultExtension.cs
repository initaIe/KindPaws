namespace KindPaws.Domain.Shared.Others.Extensions;

public static class ResultExtension
{
    public static void AddErrorIfFailure(this Result<string> result, List<string> errors)
    {
        if (result.IsFailure)
            errors.Add(result.Error);
    }

    public static void AddErrorsIfFailure(this Result<List<string>> result, List<string> errors)
    {
        if (result.IsFailure)
            errors.AddRange(result.Error);
    }
}