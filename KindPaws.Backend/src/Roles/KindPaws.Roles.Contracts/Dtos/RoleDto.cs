namespace KindPaws.Roles.Contracts.Dtos;

public class RoleDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime CreationTimestamp { get; init; }
    public RolePermissionDto[] RolePermissions { get; init; } = [];
}