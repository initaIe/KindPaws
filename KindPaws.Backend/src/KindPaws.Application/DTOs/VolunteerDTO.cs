namespace KindPaws.Application.DTOs;

public class VolunteerDTO
{
    public Guid Id { get; init; }
    public SocialNetworkDTO[] SocialNetworks { get; init; }
    public RequisiteDTO[] Requisites { get; init; }
    public FullNameDTO FullName { get; init; }
    public string EmailAddress { get; init; }
    public string PhoneNumber { get; init; }
    public string? Description { get; init; }
    public AddressDTO? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public PetDTO[] Pets { get; init; }
}