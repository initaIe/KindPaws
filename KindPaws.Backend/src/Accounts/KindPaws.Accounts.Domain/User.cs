using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<Role> _roles;

    private User()
    {
    }

    public IReadOnlyList<Role> Roles => _roles;

    public static User CreateAdmin(string userName, string email, Role role)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }

    // private List<SocialNetwork> _socialNetworks = [];
    // private List<Requisite> _requisites = [];
    //
    // // ef core
    //
    // public User()
    // {
    // }
    //
    // public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    // public IReadOnlyList<Requisite> Requisites => _requisites;
}