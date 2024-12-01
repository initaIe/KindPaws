using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;

public record AddRefreshSessionCommand(
    Guid AccountId,
    Guid Jti)
    : ICommand;