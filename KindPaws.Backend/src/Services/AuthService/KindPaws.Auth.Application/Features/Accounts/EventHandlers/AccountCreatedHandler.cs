using KindPaws.Auth.Domain.AccountsManagement.Events;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Application.Features.Accounts.EventHandlers;

public class AccountCreatedHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    public Task Handle(AccountCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}