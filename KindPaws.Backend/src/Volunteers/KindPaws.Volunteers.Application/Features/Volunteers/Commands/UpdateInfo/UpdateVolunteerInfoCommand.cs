using KindPaws.Core.Abstractions.Markers;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;

public record UpdateVolunteerInfoCommand(
    Guid VolunteerId,
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<RequisiteDto> Requisites)
    : ICommand
{
    public UpdateVolunteerInfoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}