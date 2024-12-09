using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Repositories;

public class AccountsRepository : IRepository<Account, AccountId>
{
    private readonly AuthWriteDbContext _dbContext;

    public AccountsRepository(AuthWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Account, Error>> GetByIdAsync(
        AccountId accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(
                v => v.Id == accountId,
                cancellationToken);

        if (account == null)
            return GeneralErrors.RecordNotFound(
                nameof(Account),
                nameof(AccountId),
                accountId.Value);

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

