namespace KindPaws.Accounts.Infrastructure.Seeding.Configs;

public record RolePermissionDto
{
    public string RoleName { get; set; } = null!;
    public string PermissionCode { get; set; } = null!;
}