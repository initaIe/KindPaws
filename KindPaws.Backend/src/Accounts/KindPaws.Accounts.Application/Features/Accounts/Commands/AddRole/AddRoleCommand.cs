using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.AddRole;

public record AddRoleCommand(
    Guid AccountId,
    Guid RoleId)
    : ICommand;