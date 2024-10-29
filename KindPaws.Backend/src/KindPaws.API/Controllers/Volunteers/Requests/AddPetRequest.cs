using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record AddPetRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public AddPetCommand ToCommand(Guid id)
        => new(id, SpecieId, BreedId, Name);
}