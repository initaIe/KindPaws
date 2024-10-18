using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteersHandlers.UpdateMainInfo;

namespace KindPaws.API.Controllers.Volunteers;

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