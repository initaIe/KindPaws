using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.Abstractions.IntegrationEvents;

public abstract record IntegrationEvent : Event, IIntegrationEvent;