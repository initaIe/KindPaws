using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Commands.Delete;

public record DeleteRefreshSessionCommand(
    Guid AccountId,
    Guid RefreshSessionId)
    : ICommand;