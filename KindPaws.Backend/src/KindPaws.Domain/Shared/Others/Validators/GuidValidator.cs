namespace KindPaws.Domain.Shared.Others.Validators;

public static class GuidValidator
{
    public static Result<string> EmptyValidate(this Guid id)
    {
        if (Guid.Empty.Equals(id))
            return "Guid cannot be an empty Guid.";

        return true;
    }
}