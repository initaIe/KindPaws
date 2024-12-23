using KindPaws.Core.Abstractions.IntegrationEvents;
using MediatR;

namespace KindPaws.Core.Abstractions.Handlers;

public interface IIntegrationEventHandler<T> : INotificationHandler<T>
    where T : IIntegrationEvent;