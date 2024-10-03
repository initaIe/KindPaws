namespace KindPaws.Domain.Shared.Others.Validators;

public static class GuidValidator
{
    public static bool IsEmpty(Guid id)
    {
        return Guid.Empty.Equals(id);
    }
}