using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public record CreateVolunteerCommand(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand
{
    public CreateVolunteerExistenceValidationData ToExistenceValidationData()
    {
        return new CreateVolunteerExistenceValidationData(EmailAddress, PhoneNumber);
    }
}