using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken) 
    : ICommand;