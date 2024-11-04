using KindPaws.Core.Abstractions.Markers;
using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string EmailAddress,
    string PhoneNumber)
    : ICommand
{
    public CreateVolunteerExistenceValidationData ToExistenceValidationData()
        => new(EmailAddress, PhoneNumber);
}