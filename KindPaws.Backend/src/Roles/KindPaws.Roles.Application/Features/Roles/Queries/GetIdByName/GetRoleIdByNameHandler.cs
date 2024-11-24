using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Application.Features.Roles.Queries.GetIdByName;

public class GetRoleIdByNameHandler : IQueryHandler<Result<Guid, ErrorList>, GetRoleIdByNameQuery>
{
    private readonly IRolesReadDbContext _dbContext;

    public GetRoleIdByNameHandler(IRolesReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<Guid, ErrorList>> HandleAsync(
        GetRoleIdByNameQuery query,
        CancellationToken cancellationToken = default)
    {
        var roleByName = await _dbContext.Roles.FirstOrDefaultAsync(
            r=>r.Name == query.RoleName,
            cancellationToken);

        if (roleByName == null)
            return Errors.General.RecordNotFound(
                nameof(Role), 
                nameof(RoleName), 
                query.RoleName)
                .ToErrorList();

        return roleByName.Id;
    }
}