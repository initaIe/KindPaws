using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Commands.Register;

public record RegisterCommand(
    string Email,
    string UserName, 
    string Password)
    : ICommand;