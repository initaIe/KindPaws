using KindPaws.Accounts.Application.Abstractions;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class PasswordHashProvider : IPasswordHashProvider
{
    public string Get(string password)
    {
        // var passwordHasher = new PasswordHasher<object>();
        // return passwordHasher.HashPassword(null!, password);

        return "";
    }
}