using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Commands.Create;

public record CreateAccountCommand(
    string UserName,
    string EmailAddress,
    string Password)
    : ICommand;