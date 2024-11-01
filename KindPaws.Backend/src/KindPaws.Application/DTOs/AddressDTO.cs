using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.DTOs;

public record AddressDTO(
    string City,
    string Street)
{
    public static AddressDTO GetFromDomainModel(Address address)
        => new(address.City, address.Street);
}