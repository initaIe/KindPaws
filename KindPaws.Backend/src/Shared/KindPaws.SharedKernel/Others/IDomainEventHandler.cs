using MediatR;

namespace KindPaws.SharedKernel.Others;

public interface IDomainEventHandler<T> : INotificationHandler<T>
    where T : IDomainEvent;