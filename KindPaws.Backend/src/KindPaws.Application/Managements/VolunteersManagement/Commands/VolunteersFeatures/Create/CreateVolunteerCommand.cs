using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public record CreateVolunteerCommand(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand
{
    public CreateVolunteerExistenceCheckData ToExistenceCheckData()
        => new(EmailAddress, PhoneNumber);
}