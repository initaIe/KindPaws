using KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetPetsRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? SpecieId,
    Guid? BreedId,
    string? Name,
    string? SupportStatus,
    string? Color,
    int? Age,
    int? PositionFrom,
    int? PositionTo,
    Guid? VolunteerId)
{
    public GetPetsQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SortBy,
            SortDirection,
            SpecieId,
            BreedId,
            Name,
            SupportStatus,
            Color,
            Age,
            PositionFrom,
            PositionTo,
            VolunteerId);
}