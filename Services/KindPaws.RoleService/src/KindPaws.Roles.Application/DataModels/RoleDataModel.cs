namespace KindPaws.Roles.Application.DataModels;

public class RoleDataModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public IReadOnlyList<Guid> Permissions { get; init; } = [];
}