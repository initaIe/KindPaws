using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.Commands.Login;

public record LoginByEmailAddressCommand(
    string EmailAddress,
    string Password)
    : ICommand;