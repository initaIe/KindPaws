using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures.GetSpecies;

public record GetSpeciesQuery(
    int PageNumber,
    int PageSize,
    string? Name)
    : IQuery;