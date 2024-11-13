using System.Text.Json.Serialization;

namespace KindPaws.Accounts.Infrastructure.Seeding.Configs;

public class RolePermissionConfig
{
    [JsonPropertyName("roleId")] public int RoleId { get; set; }
    [JsonPropertyName("permissionId")] public int PermissionId { get; set; }
}