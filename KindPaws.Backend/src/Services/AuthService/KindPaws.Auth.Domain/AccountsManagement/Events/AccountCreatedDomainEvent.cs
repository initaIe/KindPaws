using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Domain.AccountsManagement.Events;

public record AccountCreatedDomainEvent(
    AccountId Id,
    UserName UserName,
    EmailAddress EmailAddress) : IDomainEvent;