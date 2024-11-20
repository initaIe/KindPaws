using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Commands.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken)
    : ICommand;