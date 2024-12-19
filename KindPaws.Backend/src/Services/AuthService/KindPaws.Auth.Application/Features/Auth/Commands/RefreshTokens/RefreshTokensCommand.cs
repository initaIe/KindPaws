using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.Auth.Commands.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken)
    : ICommand;