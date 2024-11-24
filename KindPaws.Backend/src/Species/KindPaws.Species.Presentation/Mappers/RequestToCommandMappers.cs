using KindPaws.Species.Application.Features.Breeds.Commands.AddBreed;
using KindPaws.Species.Application.Features.Species.Commands.CreateSpecie;
using KindPaws.Species.Contracts.Requests;

namespace KindPaws.Species.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateSpecieCommand ToCommand(this CreateSpecieRequest request)
        => new(
            request.Name,
            request.Description);

    public static AddBreedCommand ToCommand(
        this AddBreedRequest request,
        Guid specieId)
        => new(
            specieId,
            request.Name,
            request.Description);
}