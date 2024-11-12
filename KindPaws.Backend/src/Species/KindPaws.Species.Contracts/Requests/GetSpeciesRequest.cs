namespace KindPaws.Species.Contracts.Requests;

public record GetSpeciesRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? Name);