using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<Role> _roles;

    private User()
    {
    }

    // public FullName FullName { get; private set; }
    // public IReadOnlyList<SocialNetwork> SocialNetworks { get; private set; }
    // public EmailAddress EmailAddress { get; private set; }
    // public PhoneNumber PhoneNumber { get; private set; }
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
}