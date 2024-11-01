using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;

public record GetSpeciesWithPaginationAndFilterQuery(
    PaginationDTO Pagination,
    string? Name) 
    : IQuery;