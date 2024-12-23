using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Domain.AccountsManagement.Events;

public record AccountCreatedDomainEvent(
    Guid AccountId,
    string Username,
    string EmailAddress)
    : DomainEvent;