using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.Persistence.DbContexts;
using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class AccountsRepository : IRepository<Account, AccountId>
{
    private readonly AuthWriteDbContext _writeDbContext;

    public AccountsRepository(AuthWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Result<Account, Error>> GetByIdAsync(
        AccountId accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await _writeDbContext.Accounts
            .Include(account => account.RefreshSessions)
            .FirstOrDefaultAsync(
                account => account.Id == accountId,
                cancellationToken);

        if (account == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Account),
                nameof(AccountId),
                accountId.Value);

        return account;
    }

    public async Task AddAsync(
        Account account,
        CancellationToken cancellationToken = default)
    {
        await _writeDbContext.Accounts.AddAsync(account, cancellationToken);
    }

    public void Delete(Account account)
    {
        _writeDbContext.Accounts.Remove(account);
    }
}