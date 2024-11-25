using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.Login;

public record LoginByEmailAddressCommand(
    string EmailAddress,
    string Password) 
    : ICommand;