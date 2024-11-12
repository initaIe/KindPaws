namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdatePetMainInfoRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name);