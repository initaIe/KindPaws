using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.Create;

namespace KindPaws.API.Contracts.Volunteers;

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