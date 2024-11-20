namespace KindPaws.Accounts.Application.Abstractions;

public interface IPasswordHashProvider
{
    string Get(string password);
}