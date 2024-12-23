using KindPaws.SharedKernel.DDD;

namespace KindPaws.Auth.Domain.AccountsManagement.Events;

public record AccountCreatedDomainEvent(
    Guid AccountId,
    string Username,
    string EmailAddress)
    : DomainEvent;