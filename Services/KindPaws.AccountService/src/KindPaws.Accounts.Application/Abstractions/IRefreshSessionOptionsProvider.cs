namespace KindPaws.Accounts.Application.Abstractions;

public interface IRefreshSessionOptionsProvider
{
    int GetExpireInDays();
}