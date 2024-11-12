namespace KindPaws.Volunteers.Contracts.Requests;

public record GetVolunteersRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? FirstName,
    string? LastName,
    string? Patronymic);