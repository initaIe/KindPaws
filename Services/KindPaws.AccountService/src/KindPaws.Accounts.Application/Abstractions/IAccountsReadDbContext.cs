using KindPaws.Accounts.Application.DataModels;
using KindPaws.Accounts.Contracts.Dtos;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<AccountDataModel> Accounts { get; }
    IQueryable<RefreshSessionDataModel> RefreshSessions { get; }
}