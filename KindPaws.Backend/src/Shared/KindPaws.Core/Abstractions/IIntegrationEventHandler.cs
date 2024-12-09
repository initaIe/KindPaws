using MediatR;

namespace KindPaws.Core.Abstractions;

public interface IIntegrationEventHandler<T> : INotificationHandler<T>
    where T : IIntegrationEvent;