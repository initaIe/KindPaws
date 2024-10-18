using KindPaws.Application.Volunteers.PetsHandlers.Add;

namespace KindPaws.API.Controllers.Volunteers;

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