using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Auth.Domain.AccountsManagement.Events;
using KindPaws.Core.Abstractions.Handlers;
using MassTransit;
using MassTransit.DependencyInjection;

namespace KindPaws.Auth.Application.Features.Accounts.DomainEventHandlers;

public class AccountCreatedDomainEventHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publisher;

    public AccountCreatedDomainEventHandler(Bind<IAccountsMessageBus, IPublishEndpoint> publisher)
    {
        _publisher = publisher.Value;
    }

    public async Task Handle(
        AccountCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var integrationEvent = new AccountCreatedIntegrationEvent(
            domainEvent.EventId,
            domainEvent.EventOccurredAt,
            domainEvent.AccountId,
            domainEvent.Username,
            domainEvent.EmailAddress);

        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}