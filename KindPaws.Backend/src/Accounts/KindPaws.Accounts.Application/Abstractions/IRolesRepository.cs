using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Application.Abstractions;

public interface IRolesRepository
{
    Task<Result<Role, Error>> GetByIdAsync(
        Guid roleId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Role role,
        CancellationToken cancellationToken = default);

    void Delete(Role role);
}