using KindPaws.Core.Abstractions.Markers;
using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand
{
    public UpdateVolunteerMainInfoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, EmailAddress, PhoneNumber);
}