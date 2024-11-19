using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AccountsWriteDbContext _dbContext;

    public UsersRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<User, Error>> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.Id == userId,
            cancellationToken);

        if (user == null)
            return Errors.General.RecordNotFound(
                nameof(User),
                "UserId",
                userId);

        return user;
    }

    public async Task<Result<User, Error>> GetByEmailAddressAsync(
        string emailAddress, 
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.Email == emailAddress,
            cancellationToken);

        if (user == null)
            return Errors.General.RecordNotFound(
                nameof(User),
                nameof(EmailAddress),
                emailAddress);

        return user;
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }
}