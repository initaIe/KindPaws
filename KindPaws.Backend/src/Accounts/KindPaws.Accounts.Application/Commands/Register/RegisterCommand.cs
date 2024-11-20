using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Commands.Register;

public record RegisterCommand(
    string Email,
    string UserName,
    string Password)
    : ICommand;