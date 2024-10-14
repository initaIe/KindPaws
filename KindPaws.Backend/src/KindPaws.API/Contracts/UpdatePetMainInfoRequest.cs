namespace KindPaws.API.Contracts;

public record UpdatePetMainInfoRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name);