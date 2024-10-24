namespace KindPaws.Application.DTOs;

public class VolunteerDTO
{
    public Guid Id { get; init; }
    public string? SocialNetworks { get; init; } = string.Empty;
    public string? Requisites { get; init; } = string.Empty;
    public FullNameDTO FullName { get; init; }
    public string EmailAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string? Description { get; init; } = string.Empty;
    public AddressDTO? Address { get; init; }
    public int? YearsOfExperience { get; init; }
    public IEnumerable<PetDTO> Pets { get; init; } = [];
}