using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
}