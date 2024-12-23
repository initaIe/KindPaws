using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Auth.Domain.AccountsManagement.Events;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.IntegrationEvents;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.DDD;
using MassTransit;
using MassTransit.DependencyInjection;

namespace KindPaws.Auth.Application.Features.Accounts.EventHandlers;

public class AccountCreatedDomainEventHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publisher;

    public AccountCreatedDomainEventHandler(Bind<IAuthMessageBus, IPublishEndpoint> publisher)
    {
        _publisher = publisher.Value;
    }

    public async Task Handle(
        AccountCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var integrationEvent = new AccountCreatedIntegrationEvent(
            domainEvent.AccountId,
            domainEvent.Username,
            domainEvent.EmailAddress);
        
        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}