namespace KindPaws.Accounts.Contracts.Dtos;

public class RolePermissionDto
{
    public Guid Id { get; init; }
    public Guid RoleId { get; init; }
    public Guid PermissionId { get; init; }
}