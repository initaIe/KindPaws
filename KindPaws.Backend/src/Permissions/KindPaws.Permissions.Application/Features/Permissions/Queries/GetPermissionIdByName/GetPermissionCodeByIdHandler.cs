using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Permissions.Application.Features.Permissions.Queries.GetPermissionIdByName;

public class GetPermissionCodeByIdHandler : IQueryHandler<Result<Guid, ErrorList>, GetPermissionIdByCodeQuery>
{
    private readonly IPermissionsReadDbContext _dbContext;

    public GetPermissionCodeByIdHandler(IPermissionsReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        GetPermissionIdByCodeQuery query,
        CancellationToken cancellationToken = default)
    {
        var permissionByCode = await _dbContext.Permissions.FirstOrDefaultAsync(
            r => r.Code == query.PermissionCode,
            cancellationToken);

        if (permissionByCode == null)
            return Errors.General.RecordNotFound(
                    nameof(Permission),
                    nameof(PermissionCode),
                    query.PermissionCode)
                .ToErrorList();

        return permissionByCode.Id;
    }
}