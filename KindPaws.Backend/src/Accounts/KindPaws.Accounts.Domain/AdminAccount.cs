using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Domain;

public class AdminAccount
{
    public const string Admin = "Admin";

    // ef core
    private AdminAccount()
    {
    }

    public AdminAccount(User user, FullName fullName)
    {
        Id = Guid.NewGuid();
        User = user;
        FullName = fullName;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public FullName FullName { get; set; }
}