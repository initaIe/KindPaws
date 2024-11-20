namespace KindPaws.Permissions.Contracts.Dtos;

public class PermissionDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public DateTime CreationTimestamp { get; init; }
}