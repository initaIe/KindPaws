namespace KindPaws.Auth.Domain.AccountsManagement.Events;

public record AccountDeletedDomainEvent(
    Guid AccountId);