using System.Text.Json.Serialization;

namespace KindPaws.Accounts.Infrastructure.Seeding.Configs;

public class RoleConfig
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
}