using KindPaws.Accounts.Application.Models;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IRefreshTokenSettingsProvider
{
    RefreshTokenSettings Get();
}