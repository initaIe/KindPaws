using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.Abstractions.IntegrationEvents;

public abstract record IntegrationEvent(
    Guid EventId,
    DateTimeOffset EventOccurredAt)
    : IIntegrationEvent;