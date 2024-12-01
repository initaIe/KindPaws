namespace KindPaws.Permissions.Application.DataModels;

public class PermissionDataModel
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
}