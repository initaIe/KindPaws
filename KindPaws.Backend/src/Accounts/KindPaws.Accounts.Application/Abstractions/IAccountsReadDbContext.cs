using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Permission;
using KindPaws.Accounts.Domain.Role;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<Permission> Permissions { get; }
    IQueryable<Account> Users { get; }
    IQueryable<Role> Roles { get; }
}