using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

public record GetPetsQuery(
    PaginationDTO Pagination,
    Guid? SpecieId,
    Guid? BreedId,
    string? Name,
    string? SupportStatus,
    string? Color,
    int? Age,
    Guid? VolunteerId)
    : IQuery;