using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;

namespace KindPaws.Accounts.Infrastructure.Managers;

public class AccountsManager
{
    private readonly AccountsWriteDbContext _dbContext;

    public AccountsManager(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await _dbContext.AdminAccounts.AddAsync(adminAccount);
        await _dbContext.SaveChangesAsync();
    }
}