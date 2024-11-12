namespace KindPaws.Volunteers.Contracts.Requests;

public record AddPetRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name);