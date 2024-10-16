using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo;

namespace KindPaws.API.Contracts.Volunteers.VolunteersRequests;

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