using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid VolunteerId,
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand
{
    public UpdateVolunteerMainInfoExistenceValidationData ToExistenceValidationData()
    {
        return new UpdateVolunteerMainInfoExistenceValidationData(VolunteerId, EmailAddress, PhoneNumber);
    }
}