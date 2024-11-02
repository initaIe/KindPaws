using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetPetsRequest(
    PaginationDTO Pagination,
    Guid? SpecieId,
    Guid? BreedId,
    string? Name,
    string? SupportStatus,
    string? Color,
    int? Age,
    Guid? VolunteerId)
{
    public GetPetsQuery ToCommand()
        => new(Pagination,
            SpecieId,
            BreedId,
            Name,
            SupportStatus,
            Color,
            Age,
            VolunteerId);
}