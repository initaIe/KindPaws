using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string EmailAddress,
    string Password)
    : ICommand;