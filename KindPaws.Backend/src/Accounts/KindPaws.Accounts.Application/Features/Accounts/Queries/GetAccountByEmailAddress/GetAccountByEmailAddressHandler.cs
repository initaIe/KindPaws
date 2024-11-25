using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.GetAccountByEmailAddress;

public class GetAccountByEmailAddressHandler 
    : IQueryHandler<Result<AccountDto, ErrorList>, GetAccountByEmailAddressQuery>
{
    private readonly IAccountsReadDbContext _dbContext;

    public GetAccountByEmailAddressHandler(IAccountsReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<AccountDto, ErrorList>> HandleAsync(
        GetAccountByEmailAddressQuery query,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(
            a => a.EmailAddress == query.EmailAddress,
            cancellationToken);

        if (account == null)
            return Errors.General.RecordNotFound(
                    nameof(Account),
                    nameof(EmailAddress),
                    query.EmailAddress)
                .ToErrorList();

        return account;
    }
}