using System.Text.Json.Serialization;

namespace KindPaws.Accounts.Infrastructure.Seeding.Configs;

public class PermissionConfig
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("code")] public string Code { get; set; }
}