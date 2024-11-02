using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetPetsRequest(
    int PageNumber,
    int PageSize,
    Guid? SpecieId,
    Guid? BreedId,
    string? Name,
    string? SupportStatus,
    string? Color,
    int? Age,
    Guid? VolunteerId)
{
    public GetPetsQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SpecieId,
            BreedId,
            Name,
            SupportStatus,
            Color,
            Age,
            VolunteerId);
}