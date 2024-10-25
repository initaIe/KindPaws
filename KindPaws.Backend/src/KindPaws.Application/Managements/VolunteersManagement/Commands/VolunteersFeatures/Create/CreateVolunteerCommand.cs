using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public record CreateVolunteerCommand(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand;