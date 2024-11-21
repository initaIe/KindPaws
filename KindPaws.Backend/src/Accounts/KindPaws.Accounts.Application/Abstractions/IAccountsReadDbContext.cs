using KindPaws.Accounts.Contracts.Dtos;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<AccountDto> Accounts { get; }
    IQueryable<RefreshSessionDto> RefreshSessions { get; }
    IQueryable<AccountRoleDto> AccountRoles { get; }
}