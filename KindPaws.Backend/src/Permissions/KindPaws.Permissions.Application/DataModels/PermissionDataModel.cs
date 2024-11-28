namespace KindPaws.Permissions.Application.DataModels;

public class PermissionDataModel
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public DateTime CreationTimestamp { get; init; }
}