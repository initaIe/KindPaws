using KindPaws.SharedKernel.Others;
using MediatR;

namespace KindPaws.Core.Abstractions;

public interface IIntegrationEvent : INotification, IEvent;