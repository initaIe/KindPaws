using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

public record GetBreedsBySpecieIdWithPaginationQuery(
    Guid SpecieId,
    PaginationDTO Pagination)
    : IQuery;