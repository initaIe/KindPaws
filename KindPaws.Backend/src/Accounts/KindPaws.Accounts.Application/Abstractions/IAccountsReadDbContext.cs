using KindPaws.Accounts.Domain.AggregateRoot;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IAccountsReadDbContext
{
    IQueryable<Account> Accounts { get; }
}