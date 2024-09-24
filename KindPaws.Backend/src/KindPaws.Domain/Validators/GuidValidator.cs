using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Validators;

public static class GuidValidator
{
    public static Result<Guid, string> Validate(this Guid id)
    {
        if (Guid.Empty.Equals(id))
        {
            var error = "Guid cannot be an empty Guid.";
            return Result.Failure<Guid, string>(error);
        }

        return Result.Success<Guid, string>(id);
    }
}