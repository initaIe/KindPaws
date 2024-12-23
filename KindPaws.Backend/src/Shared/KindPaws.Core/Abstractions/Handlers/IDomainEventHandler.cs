using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.Others;
using MediatR;

namespace KindPaws.Core.Abstractions.Handlers;

public interface IDomainEventHandler<T> : INotificationHandler<T>
    where T : IDomainEvent;