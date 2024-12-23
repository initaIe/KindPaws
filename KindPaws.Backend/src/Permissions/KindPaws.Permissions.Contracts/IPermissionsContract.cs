using KindPaws.Permissions.Contracts.Requests;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Permissions.Contracts;

public interface IPermissionsContract
{
    Task<Result<Guid, ErrorList>> CreatePermissionAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> DeletePermissionAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default);

    Task<bool> IsPermissionByIdExistsAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> GetPermissionIdByCodeAsync(
        string permissionCode,
        CancellationToken cancellationToken = default);
}