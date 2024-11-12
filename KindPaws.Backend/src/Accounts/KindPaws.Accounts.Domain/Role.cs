using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; }
}