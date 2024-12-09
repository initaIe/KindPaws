using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Users.Infrastructure.Repositories;

public class UsersRepository : IRepository<User, UserId>
{
    private readonly UsersWriteDbContext _dbContext;

    public UsersRepository(UsersWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<User, Error>> GetByIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(
                u => u.Id == userId,
                cancellationToken);

        if (user == null)
            return GeneralErrors.RecordNotFound(
                nameof(User),
                nameof(UserId),
                userId.Value);

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