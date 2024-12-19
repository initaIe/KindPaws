namespace KindPaws.Auth.Application.Abstractions;

public interface IAuthModuleOptionsProvider
{
    int GetRefreshSessionExpiresInDays();
    string GetDefaultRoleName();
}