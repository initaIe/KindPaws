namespace KindPaws.Auth.Application.Abstractions;

public interface IAuthOptionsProvider
{
    string GetDefaultAccountRoleName();
}