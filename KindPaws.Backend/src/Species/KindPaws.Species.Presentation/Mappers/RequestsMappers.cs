using KindPaws.Species.Application.Features.Breeds.Commands.Add;
using KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;
using KindPaws.Species.Application.Features.Species.Commands.Create;
using KindPaws.Species.Application.Features.Species.Queries.GetSpecies;
using KindPaws.Species.Contracts.Requests;

namespace KindPaws.Species.Presentation.Mappers;

public static class RequestsMappers
{
    public static GetBreedsQuery ToQuery(this GetBreedsRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.SpecieId,
            request.Name);

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

    public static GetSpeciesQuery ToQuery(this GetSpeciesRequest request)
        => new(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection,
            request.Name);
}