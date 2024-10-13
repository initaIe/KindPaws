namespace KindPaws.API.Contracts;

public record AddPetRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name);