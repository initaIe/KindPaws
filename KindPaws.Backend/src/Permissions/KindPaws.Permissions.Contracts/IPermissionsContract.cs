using KindPaws.Permissions.Contracts.Requests;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Permissions.Contracts;

public interface IPermissionsContract
{
    Task<Result<Guid, ErrorList>> CreatePermissionAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default);
    
    Task<Result<Guid, ErrorList>> DeletePermissionAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default);

    Task<bool> IsPermissionByIdExistAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default);
}