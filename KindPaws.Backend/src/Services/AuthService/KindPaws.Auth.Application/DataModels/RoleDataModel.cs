namespace KindPaws.Auth.Application.DataModels;

public class RoleDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }
    public string Name { get; init; } = null!;
    public IReadOnlyList<Guid> Permissions { get; init; } = [];
}