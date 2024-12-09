namespace KindPaws.Auth.Application.DataModels;

public class PermissionDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Code { get; init; } = null!;
}