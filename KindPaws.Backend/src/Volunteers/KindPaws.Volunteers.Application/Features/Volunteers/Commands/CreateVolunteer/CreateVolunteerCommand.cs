using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.CreateVolunteer;

// TODO: сделать пустым. Т.к. при создании волонтера ничего не надо
public record CreateVolunteerCommand(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<RequisiteDto> Requisites)
    : ICommand;