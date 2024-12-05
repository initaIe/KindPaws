using KindPaws.Users.Contracts.Dtos;

namespace KindPaws.Users.Application.DataModels;

public class ProfileDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset LastModifiedAt { get; init; }
    public string Gender { get; init; } = null!;
    public FullNameDto? FullName { get; init; }
    public DateTimeOffset? BirthdayAt { get; init; }
    public string? Description { get; init; }
    public AddressDto? Address { get; init; }
    public IReadOnlyList<SocialNetworkDto> SocialNetworks { get; init; } = [];
    public Guid UserId { get; init; }
}