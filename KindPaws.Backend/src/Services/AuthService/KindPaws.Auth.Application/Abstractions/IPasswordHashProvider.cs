namespace KindPaws.Auth.Application.Abstractions;

public interface IPasswordHashProvider
{
    string GenerateHash(string password);

    bool IsPasswordValid(string passwordHash, string password);
}