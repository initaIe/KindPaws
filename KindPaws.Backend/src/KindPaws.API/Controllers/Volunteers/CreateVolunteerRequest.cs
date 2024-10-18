using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteersHandlers.Create;

namespace KindPaws.API.Controllers.Volunteers;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber)
{
    public CreateVolunteerCommand ToCommand()
    {
        return new CreateVolunteerCommand(FullName, EmailAddress, PhoneNumber);
    }
}