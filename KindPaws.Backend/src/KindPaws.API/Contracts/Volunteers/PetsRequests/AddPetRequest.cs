using KindPaws.Application.Volunteers.PetHandlers.Add;

namespace KindPaws.API.Contracts.Volunteers.PetsRequests;

public record AddPetRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public AddPetCommand ToCommand(Guid id)
    {
        return new AddPetCommand(
            id,
            SpecieId,
            BreedId,
            Name);
    }
}