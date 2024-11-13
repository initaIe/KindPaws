using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain;

public class User : IdentityUser<Guid>
{
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