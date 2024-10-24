using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

namespace KindPaws.API.Controllers.Volunteers.Requests;

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