namespace KindPaws.Roles.Contracts.Dtos;

public class RolePermissionDto
{
    public Guid RoleId { get; init; }
    public Guid PermissionId { get; init; }
    public DateTime CreationTimestamp { get; init; }
}