using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

public record GetBreedsWithPaginationAndFilterQuery(
    PaginationDTO Pagination,
    Guid? SpecieId,
    string? Name)
    : IQuery;