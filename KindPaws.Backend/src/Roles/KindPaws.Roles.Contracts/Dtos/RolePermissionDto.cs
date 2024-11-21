namespace KindPaws.Roles.Contracts.Dtos;

public class RolePermissionDto
{
    public Guid Id { get; init; }
    public Guid PermissionId { get; init; }
    public DateTime CreationTimestamp { get; init; }
    public Guid RoleId { get; init; }
}