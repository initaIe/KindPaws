namespace KindPaws.Roles.Application.DataModels;

public class RoleDataModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime CreationTimestamp { get; init; }
    public IReadOnlyList<Guid> Permissions { get; init; } = [];
}