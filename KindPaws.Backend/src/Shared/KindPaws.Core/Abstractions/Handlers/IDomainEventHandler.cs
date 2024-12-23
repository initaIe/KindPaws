using KindPaws.SharedKernel.DDD;
using MediatR;

namespace KindPaws.Core.Abstractions.Handlers;

public interface IDomainEventHandler<T> : INotificationHandler<T>
    where T : IDomainEvent;