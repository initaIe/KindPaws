using System.Text.Json.Serialization;

namespace KindPaws.Accounts.Infrastructure.Seeding.Configs;

public class RoleConfig
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}