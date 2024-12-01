using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Auth.Application.Features.Commands.Register;

public record RegisterCommand(
    string UserName,
    string EmailAddress,
    string Password)
    : ICommand;