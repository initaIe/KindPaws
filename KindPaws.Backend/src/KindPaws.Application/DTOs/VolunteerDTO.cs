namespace KindPaws.Application.DTOs;

public class VolunteerDTO
{
    public Guid Id { get; init; }
    public IReadOnlyList<SocialNetworkDTO> SocialNetworks { get; init; } 
    public IReadOnlyList<RequisiteDTO> Requisites { get; init; } 
    public FullNameDTO FullName { get; init; }
    public string EmailAddress { get; init; } 
    public string PhoneNumber { get; init; } 
    public string? Description { get; init; } 
    public AddressDTO? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public IReadOnlyList<PetDTO> Pets { get; init; } 
}