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
    DateTime? Age,
    int? PositionFrom,
    int? PositionTo,
    Guid? VolunteerId);