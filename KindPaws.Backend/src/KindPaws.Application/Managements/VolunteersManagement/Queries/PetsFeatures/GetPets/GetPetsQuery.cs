using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

public record GetPetsQuery(
    int PageNumber,
    int PageSize,
    Guid? SpecieId,
    Guid? BreedId,
    string? Name,
    string? SupportStatus,
    string? Color,
    int? Age,
    Guid? VolunteerId)
    : IQuery;