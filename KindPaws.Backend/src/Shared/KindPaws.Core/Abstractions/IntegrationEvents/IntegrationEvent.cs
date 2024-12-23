using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Core.Abstractions.IntegrationEvents;

public abstract record IntegrationEvent : Event, IIntegrationEvent;