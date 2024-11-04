using KindPaws.Core.Dtos;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record UpdateVolunteerMainInfoRequest(
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber)
{
    public UpdateVolunteerMainInfoCommand ToCommand(Guid id)
        => new(id, FullName, EmailAddress, PhoneNumber);
}