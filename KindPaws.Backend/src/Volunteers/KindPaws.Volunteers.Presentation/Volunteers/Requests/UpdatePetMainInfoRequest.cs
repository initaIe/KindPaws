using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record UpdatePetMainInfoRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public UpdatePetMainInfoCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, SpecieId, BreedId, Name);
}