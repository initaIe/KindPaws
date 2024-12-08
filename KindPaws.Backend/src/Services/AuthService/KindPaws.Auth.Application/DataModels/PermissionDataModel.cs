namespace KindPaws.Auth.Application.DataModels;

public class PermissionDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }
    public string Code { get; init; } = null!;
}