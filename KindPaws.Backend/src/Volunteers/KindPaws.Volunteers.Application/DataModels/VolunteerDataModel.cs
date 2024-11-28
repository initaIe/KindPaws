using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.DataModels;

public class VolunteerDataModel
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public AddressDto? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public IReadOnlyList<RequisiteDto> Requisites { get; init; } = [];
    public IReadOnlyList<PetDataModel> Pets { get; init; } = [];
    public DateTimeOffset CreatedAt { get; init; }
    public bool IsSoftDeleted { get; init; }
    public DateTimeOffset? SoftDeletedAt { get; init; }
}