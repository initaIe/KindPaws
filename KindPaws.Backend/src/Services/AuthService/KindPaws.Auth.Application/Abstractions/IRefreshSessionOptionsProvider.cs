namespace KindPaws.Auth.Application.Abstractions;

public interface IRefreshSessionOptionsProvider
{
    int GetExpireInDays();
}