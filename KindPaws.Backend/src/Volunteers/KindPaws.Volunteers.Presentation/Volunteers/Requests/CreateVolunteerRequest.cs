using KindPaws.Core.Dtos;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber)
{
    public CreateVolunteerCommand ToCommand()
        => new(FullName, EmailAddress, PhoneNumber);
}