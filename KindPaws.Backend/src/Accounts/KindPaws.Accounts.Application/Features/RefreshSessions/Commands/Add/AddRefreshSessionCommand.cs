using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Commands.Add;

public record AddRefreshSessionCommand(
    Guid AccountId,
    Guid Jti)
    : ICommand;