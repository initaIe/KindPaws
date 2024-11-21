using KindPaws.Roles.Contracts.Requests;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Roles.Contracts;

public interface IRolesContract
{
    Task<Result<Guid, ErrorList>> CreateRoleAsync(CreateRoleRequest request);
    Task<Result<Guid, ErrorList>> DeleteRoleAsync(Guid roleId);
    Task<bool> IsRoleByIdExist(Guid roleId);
}