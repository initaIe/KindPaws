using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record UpdateVolunteerMainInfoRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
{
    public UpdateVolunteerMainInfoCommand ToCommand(Guid id)
    {
        return new UpdateVolunteerMainInfoCommand(
            id,
            FullName,
            EmailAddress,
            PhoneNumber);
    }
}