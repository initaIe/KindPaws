namespace KindPaws.Accounts.Application.Abstractions;

public interface IPasswordHashProvider
{
    string GenerateHash(string password);

    bool ValidateHash(string passwordHash, string password);
}