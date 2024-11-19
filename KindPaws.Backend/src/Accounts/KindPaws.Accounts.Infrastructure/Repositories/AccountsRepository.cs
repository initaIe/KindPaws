using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class AccountsRepository : IRepository<Account, Guid>
{
    private readonly AccountsWriteDbContext _dbContext;

    public AccountsRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Account, Error>> GetByIdAsync(
        Guid accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.Id == accountId,
            cancellationToken);

        if (account == null)
            return Errors.General.RecordNotFound(
                nameof(Account),
                "AccountId",
                accountId);

        return account;
    }

    public async Task<Result<Account, Error>> GetByEmailAddressAsync(
        string emailAddress, 
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.Email == emailAddress,
            cancellationToken);

        if (account == null)
            return Errors.General.RecordNotFound(
                nameof(Account),
                nameof(EmailAddress),
                emailAddress);

        return account;
    }

    public async Task AddAsync(
        Account account,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(account, cancellationToken);
    }

    public void Delete(Account account)
    {
        _dbContext.Users.Remove(account);
    }
}