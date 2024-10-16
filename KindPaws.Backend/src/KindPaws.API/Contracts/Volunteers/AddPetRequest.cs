using KindPaws.Application.Volunteers.PetHandlers.Add;

namespace KindPaws.API.Contracts.Volunteers;

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