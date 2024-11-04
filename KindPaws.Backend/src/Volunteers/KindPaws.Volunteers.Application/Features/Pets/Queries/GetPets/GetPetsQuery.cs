using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;

public record GetPetsQuery(
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
    : IQuery;