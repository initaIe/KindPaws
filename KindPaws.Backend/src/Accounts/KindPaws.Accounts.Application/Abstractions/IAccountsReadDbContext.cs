using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.Entities;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<AccountDto> Accounts { get; }
    IQueryable<RefreshSessionDto> RefreshSessions { get; }
    IQueryable<AccountRoleDto> AccountRoles { get; }
}