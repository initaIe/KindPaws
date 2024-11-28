namespace KindPaws.Volunteers.Contracts.Requests;

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
    DateTimeOffset? Age,
    int? PositionFrom,
    int? PositionTo,
    Guid? VolunteerId);