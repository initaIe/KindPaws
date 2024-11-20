namespace KindPaws.Volunteers.Contracts.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public RequisiteDto[] Requisites { get; init; } = [];
    public string? Description { get; init; }
    public AddressDto? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public PetDto[] Pets { get; init; } = [];
    public bool IsSoftDeleted { get; init; }
}