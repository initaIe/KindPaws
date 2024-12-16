using KindPaws.Accounts.Application.DataModels;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<AccountDataModel> Accounts { get; }
    IQueryable<RefreshSessionDataModel> RefreshSessions { get; }
}