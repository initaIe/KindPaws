using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Commands.Login;

public record LoginCommand(
    string Email,
    string Password)
    : ICommand;