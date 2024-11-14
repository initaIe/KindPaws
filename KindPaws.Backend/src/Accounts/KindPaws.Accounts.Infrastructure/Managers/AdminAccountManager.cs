using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;

namespace KindPaws.Accounts.Infrastructure.Managers;

public class AdminAccountManager
{
    private readonly AccountsWriteDbContext _dbContext;

    public AdminAccountManager(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await _dbContext.AddAsync(adminAccount);
        await _dbContext.SaveChangesAsync();
    }
}