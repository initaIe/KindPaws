using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.AddRefreshSessionToken;

public record AddRefreshSessionCommand(
    Guid AccountId,
    Guid Jti)
    :ICommand;