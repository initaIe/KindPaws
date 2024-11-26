using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Queries.GetRefreshSessionByAccountId;

public class GetRefreshSessionByAccountIdHandler 
    : IQueryHandler<Result<RefreshSessionDto, ErrorList>, GetRefreshSessionByAccountIdQuery>
{
    private readonly IAccountsReadDbContext _dbContext;

    public GetRefreshSessionByAccountIdHandler(IAccountsReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RefreshSessionDto, ErrorList>> HandleAsync(
        GetRefreshSessionByAccountIdQuery query, 
        CancellationToken cancellationToken = default)
    {
        var refreshSession = await _dbContext.RefreshSessions.FirstOrDefaultAsync(
            rs=>rs.AccountId == query.AccountId, 
            cancellationToken);
        
        if (refreshSession == null)
            return Errors.General.RecordNotFound(
                nameof(RefreshSession),
                nameof(AccountId),
                query.AccountId)
                .ToErrorList();

        return refreshSession;
    }
}