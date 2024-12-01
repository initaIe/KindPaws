using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class AccountsRepository : IRepository<Account, AccountId>
{
    private readonly AccountsWriteDbContext _dbContext;

    public AccountsRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Account, Error>> GetByIdAsync(
        AccountId accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts
            .Include(a => a.RefreshSessions)
            .FirstOrDefaultAsync(
                u => u.Id == accountId,
                cancellationToken);

        if (account == null)
            return Errors.General.RecordNotFound(
                nameof(Account),
                nameof(AccountId),
                accountId);

        return account;
    }

    public async Task AddAsync(
        Account account,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Accounts.AddAsync(account, cancellationToken);
    }

    public void Delete(Account account)
    {
        _dbContext.Accounts.Remove(account);
    }
}