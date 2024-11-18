using KindPaws.Core.Abstractions.Markers;
using KindPaws.Core.Dtos;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public record CreateVolunteerCommand(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<RequisiteDto> Requisites)
    : ICommand;