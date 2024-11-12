namespace KindPaws.Species.Contracts.Requests;

public record GetBreedsRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? SpecieId,
    string? Name);