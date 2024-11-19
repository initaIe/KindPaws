using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IUsersRepository
{
    Task<Result<User, Error>> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
    
    Task<Result<User, Error>> GetByEmailAddressAsync(
        string emailAddress,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        User user,
        CancellationToken cancellationToken = default);

    void Delete(User user);
}