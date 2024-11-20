namespace KindPaws.Volunteers.Contracts.Dtos;

public record AddressDto
{
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
}