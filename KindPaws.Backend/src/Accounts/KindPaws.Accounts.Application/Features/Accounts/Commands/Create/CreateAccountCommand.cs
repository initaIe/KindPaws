using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.Create;

public record CreateAccountCommand(
    string EmailAddress,
    string UserName,
    string Password) 
    : ICommand;