using KindPaws.Core.Abstractions.IntegrationEvents;

namespace KindPaws.Auth.Contracts.Messaging;

public record AccountCreatedIntegrationEvent(
    Guid AccountId,
    string Username,
    string EmailAddress)
    : IntegrationEvent;