namespace KindPaws.Core.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public SocialNetworkDto[] SocialNetworks { get; init; }
    public RequisiteDto[] Requisites { get; init; }
    public FullNameDto FullName { get; init; }
    public string EmailAddress { get; init; }
    public string PhoneNumber { get; init; }
    public string? Description { get; init; }
    public AddressDto? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public PetDto[] Pets { get; init; }
    public bool IsSoftDeleted { get; init; }
}